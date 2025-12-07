using FluentResults;
using MediatR;
using Microsoft.Extensions.Logging;
using MusicMicroservice.Application.Common.Errors;
using MusicMicroservice.Application.Common.Interfaces.Persistance;
using MusicMicroservice.Contracts.Responses.Executor;
using MusicMicroservice.Contracts.Responses.Music;
using MusicMicroservice.Domain.Entities;

namespace MusicMicroservice.Application.MusicService.Queries
{
    public class GetAllMusicWithExecutorQueryHandler : IRequestHandler<GetAllMusicWithExecutorQuery, Result<IEnumerable<MusicWithExecutorsResponse>>>
    {
        private readonly IMusicRepository _musicRepository;
        private readonly ILogger<GetAllMusicWithExecutorQueryHandler> _logger;

        public GetAllMusicWithExecutorQueryHandler(IMusicRepository musicRepository, ILogger<GetAllMusicWithExecutorQueryHandler> logger)
        {
            _musicRepository = musicRepository;
            _logger = logger;
        }
        public async Task<Result<IEnumerable<MusicWithExecutorsResponse>>> Handle(GetAllMusicWithExecutorQuery query, CancellationToken cancellationToken)
        {
            try
            {
                var musics = await _musicRepository.GetAllAsync(true, false, cancellationToken);

                
                return Result.Ok(musics.Select (m => MapToResponseWithExecutors(m)));  
                
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Unexpected error get all music");
                return Result.Fail<IEnumerable<MusicWithExecutorsResponse>>(new DatabaseError(ex.Message));
            }
        }

        private MusicWithExecutorsResponse MapToResponseWithExecutors(Music music)
        {
            return new MusicWithExecutorsResponse(
                music.Id, 
                music.Name, 
                music.Year,
                music.Style, 
                music.Executors.Select(
                    e => new ExecutorResponse(
                        e.Id, 
                        e.FirstName, 
                        e.LastName,
                        e.Nickname)).ToList()
            );
        }
    }

}