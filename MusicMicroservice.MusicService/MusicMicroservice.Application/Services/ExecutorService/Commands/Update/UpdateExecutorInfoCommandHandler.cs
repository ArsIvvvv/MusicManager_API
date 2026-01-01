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

namespace MusicMicroservice.Application.ExecutorService.Commands.Update;

public class UpdateExecutorInfoCommandHandler : IRequestHandler<UpdateExecutorInfoCommand, Result>
{
    public readonly IExecutorRepository _ExecutorRepository;
    public readonly ILogger<UpdateExecutorInfoCommandHandler> _logger;
    public readonly ICacheService _cacheService;

    public UpdateExecutorInfoCommandHandler(IExecutorRepository ExecutorRepository, ILogger<UpdateExecutorInfoCommandHandler> logger, ICacheService cacheService)
    {
        _ExecutorRepository = ExecutorRepository;
        _logger = logger;
        _cacheService = cacheService;
    }
    public async Task<Result> Handle(UpdateExecutorInfoCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var Executor =  await _ExecutorRepository.GetByIdAsync(command.Id,false, cancellationToken);
            if(Executor is null)
            {
                return Result.Fail(new NotFoundError($"Executor with id: {command.Id} not found"));
            }

            Executor.ChangeFirstName(command.FirstName);
            Executor.ChangeLastName(command.LastName);
            Executor.ChangeNickname(command.Nickname);
            
            await  _ExecutorRepository.UpdateAsync(Executor, cancellationToken);
            await _cacheService.RemoveAsync($"Executor_{command.Id}");    

            return Result.Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error update music info");
            return Result.Fail(new DatabaseError(ex.Message));
        }
    }
}