using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MusicMicroservice.Application.ExecutorService.Commands.Update;
using MusicMicroservice.Application.MusicService.Commands.Create;
using MusicMicroservice.Application.MusicService.Commands.Update;
using MusicMicroservice.Contracts.Requests.Music;

namespace MusicMicroservice.Music.API.Map.Music
{
    public static class MusicMapping
    {
        public static CreateMusicCommand ToCommandCreateMusic (this CreateMusicRequest request)
        {
            return new CreateMusicCommand
            {
                Name = request.Name,
                Year = request.Year,
                Style = request.Style,
                ExecutorIds = request.ExecutorIds
            };
        }

        public static CreateMusicWithExecutorCommand ToCommandCreateMusicWithExecutors (this CreateMusicWithExecutorsRequest request)
        {
            return new CreateMusicWithExecutorCommand
            {
                Name = request.Name,
                Year = request.Year,
                Style = request.Style,
                Executors = request.Executors
            };
        }

        public static UpdateMusicInfoCommand ToCommandUpdateMusicInfo (this UpdateMusicRequest request)
        {
            return new UpdateMusicInfoCommand
            {
                MusicId = request.Id,
                Name = request.Name,
                Year = request.Year,
                Style = request.Style,
            };
        }

        public static UpdateMusicWithExecutorsCommand ToCommandUpdateMusicWithExecutors (this UpdateMusicWithExecutorsRequest request)
        {
            return new UpdateMusicWithExecutorsCommand
            {
                MusicId = request.MusicId,
                ExecutorIds = request.ExecutorIds
            };
        }

    }
}