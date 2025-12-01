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
using MusicMicroservice.Domain.Entities;

namespace MusicMicroservice.Application.ExecutorService.Commands.Create;

public class CreateExecutorCommandHandler : ICommandHandler<CreateExecutorCommand, ExecutorResponse>
{   
    public readonly IExecutorRepository _ExecutorRepository;
    public readonly ILogger<CreateExecutorCommandHandler> _logger;

    public CreateExecutorCommandHandler(IExecutorRepository ExecutorRepository, ILogger<CreateExecutorCommandHandler> logger)
    {
        _ExecutorRepository = ExecutorRepository;
        _logger = logger;
    }

    public async Task<Result<ExecutorResponse>> Handle(CreateExecutorCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var newExecutor = Executor.Create(Guid.NewGuid(), command.FirstName, command.LastName, command.Nickname);

            await _ExecutorRepository.AddAsync(newExecutor, cancellationToken);

            return Result.Ok(MapToResponse(newExecutor));
        }
        catch(Exception ex)
        {
            _logger.LogError(ex, "Unexpected error create Executor");
            return Result.Fail<ExecutorResponse>(new DatabaseError(ex.Message));
        }
    }

     private ExecutorResponse MapToResponse(Executor Executor)
        {
            return new ExecutorResponse(Executor.Id, Executor.FirstName, Executor.LastName, Executor.Nickname);
        }
}