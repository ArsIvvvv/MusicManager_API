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
using MusicMicroservice.Domain.Entities;

namespace MusicMicroservice.Application.ExecuterService.Commands.Create;

public class CreateExecuterCommandHandler : ICommandHandler<CreateExecuterCommand, ExecuterResponse>
{   
    public readonly IExecuterRepository _executerRepository;
    public readonly ILogger<CreateExecuterCommandHandler> _logger;

    public CreateExecuterCommandHandler(IExecuterRepository executerRepository, ILogger<CreateExecuterCommandHandler> logger)
    {
        _executerRepository = executerRepository;
        _logger = logger;
    }

    public async Task<Result<ExecuterResponse>> Handle(CreateExecuterCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var newExecuter = Executor.Create(Guid.NewGuid(), command.FirstName, command.LastName, command.Nickname);

            await _executerRepository.AddAsync(newExecuter, cancellationToken);

            return Result.Ok(MapToResponse(newExecuter));
        }
        catch(Exception ex)
        {
            _logger.LogError(ex, "Unexpected error create executer");
            return Result.Fail<ExecuterResponse>(new DatabaseError(ex.Message));
        }
    }

     private ExecuterResponse MapToResponse(Executor executer)
        {
            return new ExecuterResponse(executer.Id, executer.FirstName, executer.LastName, executer.Nickname);
        }
}