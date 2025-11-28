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
using MusicMicroservice.Contracts.Responses.Music;
using MusicMicroservice.Domain.Entities;

namespace MusicMicroservice.Application.MusicService.Queries;

public class GetMusicByIdQueryHandler : IQueryHandler<GetMusicByIdQuery, Result<MusicResponse>>
{
    private readonly IMusicRepository _musicRepository;
    private readonly ICacheService _cacheService;

    private readonly ILogger<GetMusicByIdQueryHandler> _logger;

    public GetMusicByIdQueryHandler(IMusicRepository musicRepository, ICacheService cacheService, ILogger<GetMusicByIdQueryHandler> logger)
    {
        _musicRepository = musicRepository;
        _cacheService = cacheService;
        _logger = logger;
    }
    public async Task<Result<MusicResponse>> Handle(GetMusicByIdQuery query, CancellationToken cancellationToken)
    {

        var cachedMusic = await _cacheService.GetAsync<MusicResponse>($"music_{query.Id}");
        if(cachedMusic is not null)
        {
            _logger.LogInformation("Music get from Redis");
            return Result.Ok(cachedMusic);  
        }

        try
        {
            var music = await _musicRepository.GetByIdAsync(query.Id, false, cancellationToken);    
            if(music is null)
            {
                return Result.Fail<MusicResponse>(new NotFoundError($"Music with id: {query.Id} not found"));
            }

            _logger.LogInformation("Music get from Postgres");

            await _cacheService.SetAsync($"music_{query.Id}", MapToResponse(music), TimeSpan.FromMinutes(10));

            return Result.Ok(MapToResponse(music));
        }
        catch(Exception ex)
        {
            _logger.LogError(ex, "Unexpected error get music");
            return Result.Fail<MusicResponse>(new DatabaseError(ex.Message));
        }
    }
        private MusicResponse MapToResponse(Music music)
        {
            return new MusicResponse(music.Id, music.Name, music.Year, music.Style);
        }
}
