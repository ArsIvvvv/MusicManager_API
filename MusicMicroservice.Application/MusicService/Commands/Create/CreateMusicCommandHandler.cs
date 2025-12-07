using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentResults;
using MediatR;
using Microsoft.Extensions.Logging;
using MusicMicroservice.Application.Common.Errors;
using MusicMicroservice.Application.Common.Interfaces.Persistance;
using MusicMicroservice.Contracts.Responses.Music;
using MusicMicroservice.Domain.Entities;
using MusicMicroservice.Domain.Exceptions;

namespace MusicMicroservice.Application.MusicService.Commands.Create;

public class CreateMusicCommandHandler : IRequestHandler<CreateMusicCommand, Result<MusicResponse>>
{
    private readonly IMusicRepository _musicRepository;
    private readonly IExecutorRepository _ExecutorRepository;
    private readonly ILogger<CreateMusicCommandHandler> _logger;

    public CreateMusicCommandHandler(IMusicRepository musicRepository, IExecutorRepository ExecutorRepository, ILogger<CreateMusicCommandHandler> logger)
    {
        _musicRepository = musicRepository;
        _ExecutorRepository = ExecutorRepository;
        _logger = logger;
    }

    public async Task<Result<MusicResponse>> Handle(CreateMusicCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var newMusic = Music.Create(Guid.NewGuid(), command.Name, command.Year, command.Style);

            if(command.ExecutorIds is not null && command.ExecutorIds?.Count > 0)
            {
                var Executors = await _ExecutorRepository.GetRangeExecutorAsync(command.ExecutorIds, cancellationToken);
                if (Executors.Count() < 1)
                    return Result.Fail(new NotFoundError("All Executors not found."));
                var existingIds = Executors.Select(a => a.Id);
                var missingIds = command.ExecutorIds.Except(existingIds);

                if(missingIds.Count() > 0)
                    return Result.Fail(new NotFoundError("One or more Executors were not found.")
                    .WithMetadata("Missing Ids", missingIds));

                newMusic.AddRangeExecutors(Executors.ToList());
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