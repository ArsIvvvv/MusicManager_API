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

namespace MusicMicroservice.Application.ExecuterService.Queries
{
    public class GetAllExecuterQueryHandler : IQueryHandler<GetAllExecuterQuery, Result<IEnumerable<ExecuterResponse>>>
    {
        private readonly IExecuterRepository _executerRepository;
        private readonly ILogger<GetAllExecuterQueryHandler> _logger;  

        public GetAllExecuterQueryHandler(IExecuterRepository executerRepository, ILogger<GetAllExecuterQueryHandler> logger)
        {
            _executerRepository = executerRepository;
            _logger = logger;
        }

        public async Task<Result<IEnumerable<ExecuterResponse>>> Handle(GetAllExecuterQuery query, CancellationToken cancellationToken)
        {
            try
            {
                var executers = await _executerRepository.GetAllAsync(false, cancellationToken);
                
                return Result.Ok(executers.Select (e => MapToResponse(e)));  
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Unexpected error get all executers");
                return Result.Fail<IEnumerable<ExecuterResponse>>(new DatabaseError(ex.Message));
            }
        }

        private ExecuterResponse MapToResponse(Executor executer)
        {
            return new ExecuterResponse(executer.Id, executer.FirstName, executer.LastName, executer.Nickname);
        }
    }
}