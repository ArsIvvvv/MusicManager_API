using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentResults;
using Microsoft.Extensions.Logging;
using MusicMicroservice.Application.Common.Errors;
using MusicMicroservice.Application.Common.Interfaces.CQRS;
using MusicMicroservice.Application.Common.Interfaces.Persistance;

namespace MusicMicroservice.Application.ExecutorService.Commands.Update;

public class UpdateExecutorMusicsCommandHandler : ICommandHandler<UpdateExecutorMusicsCommand>
{
    private readonly IMusicRepository _musicRepository;

    private readonly IExecutorRepository _ExecutorRepository;
    private readonly ILogger<UpdateExecutorMusicsCommandHandler> _logger;

    public UpdateExecutorMusicsCommandHandler(IMusicRepository musicRepository, IExecutorRepository ExecutorRepository, ILogger<UpdateExecutorMusicsCommandHandler> logger)
    {
        _musicRepository = musicRepository;
        _ExecutorRepository = ExecutorRepository;
        _logger = logger;
    }
    public async Task<Result> Handle(UpdateExecutorMusicsCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var Executor = await _ExecutorRepository.GetByIdAsync(command.ExecutorId, false, cancellationToken);
            if (Executor is null)
                return Result.Fail(new NotFoundError($"Executor was not found."));

            var existingMusics = await _musicRepository.GetRangeBookAsync(command.MusicIds, cancellationToken);
            if (!existingMusics.Any())
                return Result.Fail(new NotFoundError($"All musics with ID not found."));

            if (existingMusics.Count() != command.MusicIds.Count)
            {
                var existingMusicsIds = existingMusics.Select(a => a.Id);
                var missingMusicsIds = command.MusicIds.Except(existingMusicsIds);

                return Result.Fail(new NotFoundError("One or more musics were not found.")
                .WithMetadata("Missing Ids", missingMusicsIds));
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