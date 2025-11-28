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
using MusicMicroservice.Application.Common.Interfaces.Services;
using MusicMicroservice.Application.MusicService.Commands.Update;

namespace MusicMicroservice.Application.MusicService.Commands.Update;

public class UpdateMusicInfoCommandHandler : ICommandHandler<UpdateMusicInfoCommand>
{
    private readonly IMusicRepository _musicRepository;
    private readonly ILogger<UpdateMusicInfoCommandHandler> _logger;

    private readonly ICacheService _cacheService;

    public UpdateMusicInfoCommandHandler(IMusicRepository musicRepository, ILogger<UpdateMusicInfoCommandHandler> logger, ICacheService cacheService)
    {
        _musicRepository = musicRepository;
        _logger = logger;
        _cacheService = cacheService;
    }
    public async Task<Result> Handle(UpdateMusicInfoCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var music =  await _musicRepository.GetByIdAsync(command.MusicId,false, cancellationToken);
            if(music is null)
            {
                return Result.Fail(new NotFoundError($"Music with id: {command.MusicId} not found"));
            }

            music.ChangeName(command.Name);
            music.ChangeYear(command.Year);
            music.ChangeStyle(command.Style);
            
            await  _musicRepository.UpdateAsync(music, cancellationToken);

            await _cacheService.RemoveAsync($"music_{command.MusicId}");    

            return Result.Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error update music info");
            return Result.Fail(new DatabaseError(ex.Message));
        }
    }
}