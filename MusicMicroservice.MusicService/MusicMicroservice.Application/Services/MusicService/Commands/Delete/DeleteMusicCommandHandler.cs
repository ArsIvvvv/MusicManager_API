using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentResults;
using MediatR;
using Microsoft.Extensions.Logging;
using MusicMicroservice.Application.Common.Errors;
using MusicMicroservice.Application.Common.Interfaces.Persistance;
using MusicMicroservice.Application.Common.Interfaces.Persistance.Redis;

namespace MusicMicroservice.Application.MusicService.Commands.Delete;

public class DeleteMusicCommandHandler : IRequestHandler<DeleteMusicCommand, Result>
{
    private readonly IMusicRepository _musicRepository;
    private readonly ILogger<DeleteMusicCommandHandler> _logger;
    private readonly ICacheService  _cacheService;

    public DeleteMusicCommandHandler(IMusicRepository musicRepository, ILogger<DeleteMusicCommandHandler> logger, ICacheService cacheService)
    {
        _musicRepository = musicRepository;
        _logger = logger;
        _cacheService = cacheService;
    }
    public async Task<Result> Handle(DeleteMusicCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var music = await _musicRepository.GetByIdAsync(command.Id, false, cancellationToken);  
              
            if(music is null)
            {
                return Result.Fail(new NotFoundError($"Music with id: {command.Id} not found"));
            }

            await _musicRepository.DeleteAsync(music, cancellationToken);

            await _cacheService.RemoveAsync($"music_{command.Id}");

            return Result.Ok();
        }
        catch(Exception ex)
        {
            _logger.LogError(ex, "Unexpected error delete music");
            return Result.Fail(new DatabaseError(ex.Message));
        }   
    }
}