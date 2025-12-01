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
using MusicMicroservice.Contracts.Responses.Music;
using MusicMicroservice.Domain.Entities;

namespace MusicMicroservice.Application.ExecutorService.Queries
{
    public class GetAllExecutorWithMusicQueryHandler : IQueryHandler<GetAllExecutorWithMusicQuery, Result<IEnumerable<ExecutorWithMusicResponse>>>
    {
        private readonly IExecutorRepository _ExecutorRepository;

        private readonly ILogger<GetAllExecutorWithMusicQueryHandler> _logger;


       public async Task<Result<IEnumerable<ExecutorWithMusicResponse>>> Handle(GetAllExecutorWithMusicQuery query, CancellationToken cancellationToken)
        {
            try
            {
                var musics = await _ExecutorRepository.GetAllAsync(true, cancellationToken);
                
                return Result.Ok(musics.Select (m => MapToResponseWithExecutors(m)));  
                
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Unexpected error get all music");
                return Result.Fail<IEnumerable<ExecutorWithMusicResponse>>(new DatabaseError(ex.Message));
            }
        }

        private ExecutorWithMusicResponse MapToResponseWithExecutors(Executor Executor)
        {
            return new ExecutorWithMusicResponse(
                Executor.Id, 
                Executor.FirstName, 
                Executor.LastName,
                Executor.Nickname, 
                Executor.Musics.Select(
                    m => new MusicResponse(
                        m.Id, 
                        m.Name, 
                        m.Year,
                        m.Style)).ToList()
            );
        }
    }
}