using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentResults;
using Microsoft.Extensions.Logging;
using MusicMicroservice.Application.Common.Errors;
using MusicMicroservice.Application.Common.Interfaces.CQRS;
using MusicMicroservice.Application.Common.Interfaces.Persistance;

namespace MusicMicroservice.Application.MusicService.Commands.Update;

public class UpdateMusicWithExecutersCommandHandler : ICommandHandler<UpdateMusicWithExecutersCommand>
{
    private readonly IMusicRepository _musicRepository;

    private readonly IExecuterRepository _executerRepository;
    private readonly ILogger<UpdateMusicWithExecutersCommandHandler> _logger;

    public UpdateMusicWithExecutersCommandHandler(IMusicRepository musicRepository, IExecuterRepository executerRepository, ILogger<UpdateMusicWithExecutersCommandHandler> logger)
    {
        _musicRepository = musicRepository;
        _executerRepository = executerRepository;
        _logger = logger;
    }
    public async Task<Result> Handle(UpdateMusicWithExecutersCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var music = await _musicRepository.GetByIdAsync(command.MusicId, false, cancellationToken);
            if (music is null)
                return Result.Fail(new NotFoundError($"Music was not found."));

            var existingExecuters = await _executerRepository.GetRangeExecuterAsync(command.ExecuterId, cancellationToken);
            if (!existingExecuters.Any())
                return Result.Fail(new NotFoundError($"All executers with ID not found."));

            if (existingExecuters.Count() != command.ExecuterId.Count)
            {
                var existingExecutersIds = existingExecuters.Select(a => a.Id);
                var missingExecutersIds = command.ExecuterId.Except(existingExecutersIds);

                return Result.Fail(new NotFoundError("One or more authors were not found.")
                .WithMetadata("Missing Ids", missingExecutersIds));
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