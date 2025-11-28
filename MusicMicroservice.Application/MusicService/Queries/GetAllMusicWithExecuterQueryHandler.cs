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

namespace MusicMicroservice.Application.MusicService.Queries
{
    public class GetAllMusicWithExecuterQueryHandler : IQueryHandler<GetAllMusicWithExecuterQuery, Result<IEnumerable<MusicWithExecutersResponse>>>
    {
        private readonly IMusicRepository _musicRepository;
        private readonly ILogger<GetAllMusicWithExecuterQueryHandler> _logger;

        public GetAllMusicWithExecuterQueryHandler(IMusicRepository musicRepository, ILogger<GetAllMusicWithExecuterQueryHandler> logger)
        {
            _musicRepository = musicRepository;
            _logger = logger;
        }
        public async Task<Result<IEnumerable<MusicWithExecutersResponse>>> Handle(GetAllMusicWithExecuterQuery query, CancellationToken cancellationToken)
        {
            try
            {
                var musics = await _musicRepository.GetAllAsync(true, cancellationToken);
                
                return Result.Ok(musics.Select (m => MapToResponseWithExecuters(m)));  
                
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Unexpected error get all music");
                return Result.Fail<IEnumerable<MusicWithExecutersResponse>>(new DatabaseError(ex.Message));
            }
        }

        private MusicWithExecutersResponse MapToResponseWithExecuters(Music music)
        {
            return new MusicWithExecutersResponse(
                music.Id, 
                music.Name, 
                music.Year,
                music.Style, 
                music.Executors.Select(
                    e => new ExecuterResponse(
                        e.Id, 
                        e.FirstName, 
                        e.LastName,
                        e.Nickname)).ToList()
            );
        }
    }

}