using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentResults;
using MediatR;
using Microsoft.Extensions.Logging;
using MusicMicroservice.Application.Common.Errors;
using MusicMicroservice.Application.Common.Interfaces.Persistance;

namespace MusicMicroservice.Application.MusicService.Commands.Update;

public class UpdateMusicWithExecutorsCommandHandler : IRequestHandler<UpdateMusicWithExecutorsCommand, Result>
{
    private readonly IMusicRepository _musicRepository;

    private readonly IExecutorRepository _ExecutorRepository;

    private readonly ILogger<UpdateMusicWithExecutorsCommandHandler> _logger;

    public UpdateMusicWithExecutorsCommandHandler(IMusicRepository musicRepository, IExecutorRepository ExecutorRepository, ILogger<UpdateMusicWithExecutorsCommandHandler> logger)
    {
        _musicRepository = musicRepository;
        _ExecutorRepository = ExecutorRepository;
        _logger = logger;
    }
    
    public async Task<Result> Handle(UpdateMusicWithExecutorsCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var music = await _musicRepository.GetByIdAsync(command.MusicId, false, cancellationToken);
            if (music is null)
                return Result.Fail(new NotFoundError($"Music was not found."));

            var existingExecutors = await _ExecutorRepository.GetRangeExecutorAsync(command.ExecutorIds, cancellationToken);
            if (!existingExecutors.Any())
                return Result.Fail(new NotFoundError($"All Executors with ID not found."));

            if (existingExecutors.Count() != command.ExecutorIds.Count)
            {
                var existingExecutorsIds = existingExecutors.Select(a => a.Id);
                var missingExecutorsIds = command.ExecutorIds.Except(existingExecutorsIds);

                return Result.Fail(new NotFoundError("One or more authors were not found.")
                .WithMetadata("Missing Ids", missingExecutorsIds));
            } 

            return Result.Ok();
        }
        catch(Exception ex)
        {
            _logger.LogError(ex, "Unexpected error update music");
            return Result.Fail(new DatabaseError(ex.Message));
        }
    }
}