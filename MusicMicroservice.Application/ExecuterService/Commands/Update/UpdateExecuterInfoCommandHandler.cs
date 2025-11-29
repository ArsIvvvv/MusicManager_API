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

namespace MusicMicroservice.Application.ExecuterService.Commands.Update;

public class UpdateExecuterInfoCommandHandler : ICommandHandler<UpdateExecuterInfoCommand>
{
    public readonly IExecuterRepository _executerRepository;
    public readonly ILogger<UpdateExecuterInfoCommandHandler> _logger;
    public readonly ICacheService _cacheService;

    public UpdateExecuterInfoCommandHandler(IExecuterRepository executerRepository, ILogger<UpdateExecuterInfoCommandHandler> logger, ICacheService cacheService)
    {
        _executerRepository = executerRepository;
        _logger = logger;
        _cacheService = cacheService;
    }
    public async Task<Result> Handle(UpdateExecuterInfoCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var executer =  await _executerRepository.GetByIdAsync(command.ExecuterId,false, cancellationToken);
            if(executer is null)
            {
                return Result.Fail(new NotFoundError($"Executer with id: {command.ExecuterId} not found"));
            }

            executer.ChangeFirstName(command.FirstName);
            executer.ChangeLastName(command.LastName);
            executer.ChangeNickname(command.Nickname);
            
            await  _executerRepository.UpdateAsync(executer, cancellationToken);
            await _cacheService.RemoveAsync($"executer_{command.ExecuterId}");    

            return Result.Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error update music info");
            return Result.Fail(new DatabaseError(ex.Message));
        }
    }
}