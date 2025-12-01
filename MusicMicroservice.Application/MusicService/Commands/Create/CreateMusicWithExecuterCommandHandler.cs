using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentResults;
using Microsoft.Extensions.Logging;
using MusicMicroservice.Application.Common.Errors;
using MusicMicroservice.Application.Common.Interfaces.CQRS;
using MusicMicroservice.Application.Common.Interfaces.Persistance;
using MusicMicroservice.Contracts.Responses.Executor;
using MusicMicroservice.Contracts.Responses.Music;
using MusicMicroservice.Domain.Entities;
using MusicMicroservice.Domain.Exceptions;

namespace MusicMicroservice.Application.MusicService.Commands.Create;

public class CreateMusicWithExecutorCommandHandler : ICommandHandler<CreateMusicWithExecutorCommand, MusicWithExecutorsResponse>
{
    private readonly IMusicRepository _musicRepository;
    private readonly ILogger<CreateMusicWithExecutorCommandHandler> _logger;

    public CreateMusicWithExecutorCommandHandler(IMusicRepository musicRepository, ILogger<CreateMusicWithExecutorCommandHandler> logger)
    {
        _musicRepository = musicRepository;
        _logger = logger;
    }

    public async Task<Result<MusicWithExecutorsResponse>> Handle(CreateMusicWithExecutorCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var newMusic = Music.Create(Guid.NewGuid(), command.Name, command.Year, command.Style);
            if(command.Executors is not null && command.Executors?.Count > 0)
            {
                var newExecutors = new List<Executor>();
                foreach(var Executor in command.Executors)
                {
                    var newExecutor = Executor.Create(Guid.NewGuid(), 
                    Executor.FirstName,
                    Executor.LastName,
                    Executor.Nickname);
                        
                    newExecutors.Add(newExecutor);
                }
                newMusic.AddRangeExecutors(newExecutors);
            }
            await _musicRepository.AddAsync(newMusic, cancellationToken);

            return Result.Ok(MapToResponseWithExecutors(newMusic));
        }
        catch(DomainException ex)
        {
            return Result.Fail(new ValidationError(ex.Message));
        }
        catch(Exception ex)
        {
            _logger.LogError(ex, "Unexpected error create music with Executors");
            return Result.Fail<MusicWithExecutorsResponse>(new DatabaseError(ex.Message));
        } 
    }
    private MusicWithExecutorsResponse MapToResponseWithExecutors(Music music)
        {
            return new MusicWithExecutorsResponse(
                music.Id, 
                music.Name, 
                music.Year,
                music.Style, 
                music.Executors.Select(
                    e => new ExecutorResponse(
                        e.Id, 
                        e.FirstName, 
                        e.LastName,
                        e.Nickname)).ToList()
            );
        }
}