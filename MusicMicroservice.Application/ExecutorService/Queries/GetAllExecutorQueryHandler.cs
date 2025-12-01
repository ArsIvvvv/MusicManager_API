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

namespace MusicMicroservice.Application.ExecutorService.Queries
{
    public class GetAllExecutorQueryHandler : IQueryHandler<GetAllExecutorQuery, Result<IEnumerable<ExecutorResponse>>>
    {
        private readonly IExecutorRepository _ExecutorRepository;
        private readonly ILogger<GetAllExecutorQueryHandler> _logger;  

        public GetAllExecutorQueryHandler(IExecutorRepository ExecutorRepository, ILogger<GetAllExecutorQueryHandler> logger)
        {
            _ExecutorRepository = ExecutorRepository;
            _logger = logger;
        }

        public async Task<Result<IEnumerable<ExecutorResponse>>> Handle(GetAllExecutorQuery query, CancellationToken cancellationToken)
        {
            try
            {
                var Executors = await _ExecutorRepository.GetAllAsync(false, cancellationToken);
                
                return Result.Ok(Executors.Select (e => MapToResponse(e)));  
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Unexpected error get all Executors");
                return Result.Fail<IEnumerable<ExecutorResponse>>(new DatabaseError(ex.Message));
            }
        }

        private ExecutorResponse MapToResponse(Executor Executor)
        {
            return new ExecutorResponse(Executor.Id, Executor.FirstName, Executor.LastName, Executor.Nickname);
        }
    }
}