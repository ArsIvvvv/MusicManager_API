using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentResults;
using Microsoft.Extensions.Logging;
using MusicMicroservice.Application.Common.Errors;
using MusicMicroservice.Application.Common.Interfaces.CQRS;
using MusicMicroservice.Application.Common.Interfaces.Persistance;
using MusicMicroservice.Contracts.Responses.Executer;
using MusicMicroservice.Contracts.Responses.Music;
using MusicMicroservice.Domain.Entities;
using MusicMicroservice.Domain.Exceptions;

namespace MusicMicroservice.Application.MusicService.Commands.Create;

public class CreateMusicWithExecuterCommandHandler : ICommandHandler<CreateMusicWithExecuterCommand, MusicWithExecutersResponse>
{
    private readonly IMusicRepository _musicRepository;
    private readonly ILogger<CreateMusicWithExecuterCommandHandler> _logger;

    public CreateMusicWithExecuterCommandHandler(IMusicRepository musicRepository, ILogger<CreateMusicWithExecuterCommandHandler> logger)
    {
        _musicRepository = musicRepository;
        _logger = logger;
    }

    public async Task<Result<MusicWithExecutersResponse>> Handle(CreateMusicWithExecuterCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var newMusic = Music.Create(Guid.NewGuid(), command.Name, command.Year, command.Style);
            if(command.Executers is not null && command.Executers?.Count > 0)
            {
                var newExecuters = new List<Executor>();
                foreach(var executer in command.Executers)
                {
                    var newExecuter = Executor.Create(Guid.NewGuid(), 
                    executer.FirstName,
                    executer.LastName,
                    executer.Nickname);
                        
                    newExecuters.Add(newExecuter);
                }
                newMusic.AddRangeExecutors(newExecuters);
            }
            await _musicRepository.AddAsync(newMusic, cancellationToken);

            return Result.Ok(MapToResponseWithExecuters(newMusic));
        }
        catch(DomainException ex)
        {
            return Result.Fail(new ValidationError(ex.Message));
        }
        catch(Exception ex)
        {
            _logger.LogError(ex, "Unexpected error create music with executers");
            return Result.Fail<MusicWithExecutersResponse>(new DatabaseError(ex.Message));
        } 
    }
    private MusicWithExecutersResponse MapToResponseWithExecuters(Music music)
        {
            return new MusicWithExecutersResponse(
                music.Id, 
                music.Name, 
                music.Year,
                music.Style, 
                music.Executors.Select(
                    e => new ExecuterResponse(
                        e.Id, 
                        e.FirstName, 
                        e.LastName,
                        e.Nickname)).ToList()
            );
        }
}