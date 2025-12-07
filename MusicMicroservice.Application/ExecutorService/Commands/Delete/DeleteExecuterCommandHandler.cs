using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentResults;
using MediatR;
using Microsoft.Extensions.Logging;
using MusicMicroservice.Application.Common.Errors;
using MusicMicroservice.Application.Common.Interfaces.Persistance;
using MusicMicroservice.Application.Common.Interfaces.Persistance.Redis;

namespace MusicMicroservice.Application.ExecutorService.Commands.Delete;

public class DeleteExecutorCommandHandler : IRequestHandler<DeleteExecutorCommand, Result>
{
    public readonly IExecutorRepository _ExecutorRepository;
    public readonly ILogger<DeleteExecutorCommandHandler> _logger;
    public readonly ICacheService _cacheService;
    
    public async Task<Result> Handle(DeleteExecutorCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var Executor = await _ExecutorRepository.GetByIdAsync(command.Id, false, cancellationToken);    
            if(Executor is null)
            {
                return Result.Fail(new NotFoundError($"Executor with id: {command.Id} not found"));
            }

            await _ExecutorRepository.DeleteAsync(Executor, cancellationToken);

            await _cacheService.RemoveAsync($"Executor_{command.Id}");

            return Result.Ok();
        }
        catch(Exception ex)
        {
            _logger.LogError(ex, "Unexpected error delete music");
            return Result.Fail(new DatabaseError(ex.Message));
        }   
    }
}