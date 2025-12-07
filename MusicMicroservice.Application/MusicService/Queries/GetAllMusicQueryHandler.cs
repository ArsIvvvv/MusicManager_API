using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentResults;
using MediatR;
using Microsoft.Extensions.Logging;
using MusicMicroservice.Application.Common.Errors;
using MusicMicroservice.Application.Common.Interfaces.Persistance;
using MusicMicroservice.Contracts.Responses.Music;
using MusicMicroservice.Domain.Entities;

namespace MusicMicroservice.Application.MusicService.Queries;

public class GetAllMusicQueryHandler : IRequestHandler<GetAllMusicQuery, Result<IEnumerable<MusicResponse>>>
{
    private readonly IMusicRepository _musicRepository;
    private readonly ILogger<GetAllMusicQueryHandler> _logger;

    public GetAllMusicQueryHandler(IMusicRepository musicRepository, ILogger<GetAllMusicQueryHandler> logger)
    {
        _musicRepository = musicRepository;
        _logger = logger;
    }

    public async Task<Result<IEnumerable<MusicResponse>>> Handle(GetAllMusicQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var musics = await _musicRepository.GetAllAsync(false, false,cancellationToken);

            if(musics is null)
            {
                return Result.Fail<IEnumerable<MusicResponse>>(new NotFoundError($"Musics not found"));
            }
    
            return Result.Ok(musics.Select (m => MapToResponse(m)));  
        }
        catch(Exception ex)
        {
            _logger.LogError(ex, "Unexpected error get all music");
            return Result.Fail<IEnumerable<MusicResponse>>(new DatabaseError(ex.Message));
        }
    }

    private MusicResponse MapToResponse(Music music)
    {
        return new MusicResponse(music.Id, music.Name, music.Year, music.Style);
    }
}