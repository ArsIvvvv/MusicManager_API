using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentResults;
using Microsoft.Extensions.Logging;
using MusicMicroservice.Application.Common.Errors;
using MusicMicroservice.Application.Common.Interfaces.CQRS;
using MusicMicroservice.Application.Common.Interfaces.Persistance;
using MusicMicroservice.Application.Common.Interfaces.Persistance.Redis;

namespace MusicMicroservice.Application.ExecuterService.Commands.Delete;

public class DeleteExecuterCommandHandler : ICommandHandler<DeleteExecuterCommand>
{
    public readonly IExecuterRepository _executerRepository;
    public readonly ILogger<DeleteExecuterCommandHandler> _logger;
    public readonly ICacheService _cacheService;
    public async Task<Result> Handle(DeleteExecuterCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var executer = await _executerRepository.GetByIdAsync(command.Id, false, cancellationToken);    
            if(executer is null)
            {
                return Result.Fail(new NotFoundError($"Executer with id: {command.Id} not found"));
            }

            await _executerRepository.DeleteAsync(executer, cancellationToken);

            await _cacheService.RemoveAsync($"executer_{command.Id}");

            return Result.Ok();
        }
        catch(Exception ex)
        {
            _logger.LogError(ex, "Unexpected error delete music");
            return Result.Fail(new DatabaseError(ex.Message));
        }   
    }
}