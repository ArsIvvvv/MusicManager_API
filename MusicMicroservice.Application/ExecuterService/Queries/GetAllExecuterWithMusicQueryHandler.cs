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
using MusicMicroservice.Contracts.Responses.Music;
using MusicMicroservice.Domain.Entities;

namespace MusicMicroservice.Application.ExecuterService.Queries
{
    public class GetAllExecuterWithMusicQueryHandler : IQueryHandler<GetAllExecuterWithMusicQuery, Result<IEnumerable<ExecuterWithMusicResponse>>>
    {
        private readonly IExecuterRepository _executerRepository;

        private readonly ILogger<GetAllExecuterWithMusicQueryHandler> _logger;


       public async Task<Result<IEnumerable<ExecuterWithMusicResponse>>> Handle(GetAllExecuterWithMusicQuery query, CancellationToken cancellationToken)
        {
            try
            {
                var musics = await _executerRepository.GetAllAsync(true, cancellationToken);
                
                return Result.Ok(musics.Select (m => MapToResponseWithExecuters(m)));  
                
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Unexpected error get all music");
                return Result.Fail<IEnumerable<ExecuterWithMusicResponse>>(new DatabaseError(ex.Message));
            }
        }

        private ExecuterWithMusicResponse MapToResponseWithExecuters(Executor executer)
        {
            return new ExecuterWithMusicResponse(
                executer.Id, 
                executer.FirstName, 
                executer.LastName,
                executer.Nickname, 
                executer.Musics.Select(
                    m => new MusicResponse(
                        m.Id, 
                        m.Name, 
                        m.Year,
                        m.Style)).ToList()
            );
        }
    }
}