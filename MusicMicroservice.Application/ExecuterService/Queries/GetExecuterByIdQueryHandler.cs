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
using MusicMicroservice.Contracts.Responses.Executer;
using MusicMicroservice.Domain.Entities;

namespace MusicMicroservice.Application.ExecuterService.Queries
{
    public class GetExecuterByIdQueryHandler : IQueryHandler<GetExecuterByIdQuery, FluentResults.Result<Contracts.Responses.Executer.ExecuterResponse>>
    {
        public readonly IExecuterRepository _executerRepository;
        public readonly ILogger<GetExecuterByIdQueryHandler> _logger;
        public readonly ICacheService _cacheService;

        public GetExecuterByIdQueryHandler(IExecuterRepository executerRepository, ILogger<GetExecuterByIdQueryHandler> logger, ICacheService cacheService)
        {
            _executerRepository = executerRepository;
            _logger = logger;
            _cacheService = cacheService;
        }
        public async Task<Result<ExecuterResponse>> Handle(GetExecuterByIdQuery query, CancellationToken cancellationToken)
        {
            var cachedExecuter = await _cacheService.GetAsync<ExecuterResponse>($"executer_{query.Id}");
            if(cachedExecuter is not null)
            {
                _logger.LogInformation("Executer get from Redis");
                return Result.Ok(cachedExecuter);  
            }

            try
            {
                var executer = await _executerRepository.GetByIdAsync(query.Id, false, cancellationToken);    
                if(executer is null)
                {
                    return Result.Fail<ExecuterResponse>(new NotFoundError($"Executer with id: {query.Id} not found"));
                }

                _logger.LogInformation("Executer get from Postgres");

                await _cacheService.SetAsync($"executer_{query.Id}", MapToResponse(executer), TimeSpan.FromMinutes(10));

                return Result.Ok(MapToResponse(executer));
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Unexpected error get executer");
                return Result.Fail<ExecuterResponse>(new DatabaseError(ex.Message));
            }
        }
        private ExecuterResponse MapToResponse(Executor executer)
        {
            return new ExecuterResponse(executer.Id, executer.FirstName, executer.LastName, executer.Nickname);
        }
    }
}