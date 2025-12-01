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
using MusicMicroservice.Contracts.Responses.Executor;
using MusicMicroservice.Domain.Entities;

namespace MusicMicroservice.Application.ExecutorService.Queries
{
    public class GetExecutorByIdQueryHandler : IQueryHandler<GetExecutorByIdQuery, FluentResults.Result<Contracts.Responses.Executor.ExecutorResponse>>
    {
        public readonly IExecutorRepository _ExecutorRepository;
        public readonly ILogger<GetExecutorByIdQueryHandler> _logger;
        public readonly ICacheService _cacheService;

        public GetExecutorByIdQueryHandler(IExecutorRepository ExecutorRepository, ILogger<GetExecutorByIdQueryHandler> logger, ICacheService cacheService)
        {
            _ExecutorRepository = ExecutorRepository;
            _logger = logger;
            _cacheService = cacheService;
        }
        public async Task<Result<ExecutorResponse>> Handle(GetExecutorByIdQuery query, CancellationToken cancellationToken)
        {
            var cachedExecutor = await _cacheService.GetAsync<ExecutorResponse>($"Executor_{query.Id}");
            if(cachedExecutor is not null)
            {
                _logger.LogInformation("Executor get from Redis");
                return Result.Ok(cachedExecutor);  
            }

            try
            {
                var Executor = await _ExecutorRepository.GetByIdAsync(query.Id, false, cancellationToken);    
                if(Executor is null)
                {
                    return Result.Fail<ExecutorResponse>(new NotFoundError($"Executor with id: {query.Id} not found"));
                }

                _logger.LogInformation("Executor get from Postgres");

                await _cacheService.SetAsync($"Executor_{query.Id}", MapToResponse(Executor), TimeSpan.FromMinutes(10));

                return Result.Ok(MapToResponse(Executor));
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Unexpected error get Executor");
                return Result.Fail<ExecutorResponse>(new DatabaseError(ex.Message));
            }
        }
        private ExecutorResponse MapToResponse(Executor Executor)
        {
            return new ExecutorResponse(Executor.Id, Executor.FirstName, Executor.LastName, Executor.Nickname);
        }
    }
}