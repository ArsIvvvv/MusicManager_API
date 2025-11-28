using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentResults;
using Microsoft.Extensions.Logging;
using MusicMicroservice.Application.Common.Errors;
using MusicMicroservice.Application.Common.Interfaces.CQRS;
using MusicMicroservice.Application.Common.Interfaces.Persistance;
using MusicMicroservice.Contracts.Responses.Music;
using MusicMicroservice.Domain.Entities;
using MusicMicroservice.Domain.Exceptions;

namespace MusicMicroservice.Application.MusicService.Commands.Create;

public class CreateMusicCommandHandler : ICommandHandler<CreateMusicCommand, MusicResponse>
{
    private readonly IMusicRepository _musicRepository;
    private readonly IExecuterRepository _executerRepository;
    private readonly ILogger<CreateMusicCommandHandler> _logger;

    public CreateMusicCommandHandler(IMusicRepository musicRepository, IExecuterRepository executerRepository, ILogger<CreateMusicCommandHandler> logger)
    {
        _musicRepository = musicRepository;
        _executerRepository = executerRepository;
        _logger = logger;
    }

    public async Task<Result<MusicResponse>> Handle(CreateMusicCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var newMusic = Music.Create(Guid.NewGuid(), command.Name, command.Year, command.Style);

            if(command.ExecuterIds is not null && command.ExecuterIds?.Count > 0)
            {
                var executers = await _executerRepository.GetRangeExecuterAsync(command.ExecuterIds, cancellationToken);
                if (executers.Count() < 1)
                    return Result.Fail(new NotFoundError("All executers not found."));
                var existingIds = executers.Select(a => a.Id);
                var missingIds = command.ExecuterIds.Except(existingIds);

                if(missingIds.Count() > 0)
                    return Result.Fail(new NotFoundError("One or more executers were not found.")
                    .WithMetadata("Missing Ids", missingIds));

                newMusic.AddRangeExecutors(executers.ToList());
            }

            await _musicRepository.AddAsync(newMusic, cancellationToken);

            return Result.Ok(MapToResponse(newMusic));
        }
        catch(DomainException ex)
        {
            return Result.Fail(new ValidationError(ex.Message));
        }
        catch(Exception ex)
        {
            _logger.LogError(ex, "Unexpected error update music");
            return Result.Fail(new DatabaseError(ex.Message));
        }
    }

    private MusicResponse MapToResponse(Music music)
    {
        return new MusicResponse(music.Id, music.Name, music.Year, music.Style);
    }
}