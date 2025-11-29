using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentResults;
using Microsoft.Extensions.Logging;
using MusicMicroservice.Application.Common.Errors;
using MusicMicroservice.Application.Common.Interfaces.CQRS;
using MusicMicroservice.Application.Common.Interfaces.Persistance;

namespace MusicMicroservice.Application.ExecuterService.Commands.Update;

public class UpdateExecuterMusicsCommandHandler : ICommandHandler<UpdateExecuterMusicsCommand>
{
    private readonly IMusicRepository _musicRepository;

    private readonly IExecuterRepository _executerRepository;
    private readonly ILogger<UpdateExecuterMusicsCommandHandler> _logger;

    public UpdateExecuterMusicsCommandHandler(IMusicRepository musicRepository, IExecuterRepository executerRepository, ILogger<UpdateExecuterMusicsCommandHandler> logger)
    {
        _musicRepository = musicRepository;
        _executerRepository = executerRepository;
        _logger = logger;
    }
    public async Task<Result> Handle(UpdateExecuterMusicsCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var executer = await _executerRepository.GetByIdAsync(command.ExecuterId, false, cancellationToken);
            if (executer is null)
                return Result.Fail(new NotFoundError($"Executer was not found."));

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