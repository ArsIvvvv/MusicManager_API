using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MusicMicroservice.Application.ExecutorService.Commands.Create;
using MusicMicroservice.Application.ExecutorService.Commands.Update;
using MusicMicroservice.Application.MusicService.Commands.Create;
using MusicMicroservice.Contracts.Requests.Executor;
using MusicMicroservice.Contracts.Requests.Music;

namespace MusicMicroservice.Music.API.Map.Executor
{
    public static class ExecutorMapping
    {
        public static CreateExecutorCommand ToCommandCreateExecutor (this CreateExecutorRequest request)
        {
            return new CreateExecutorCommand
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Nickname = request.Nickname,
            };
        }

        public static UpdateExecutorInfoCommand ToCommandUpdateExecutorInfo(this UpdateExecutorRequest request)
        {
            return new UpdateExecutorInfoCommand
            {
                Id = request.Id,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Nickname = request.Nickname,
            };
        }

        public static UpdateExecutorMusicsCommand ToCommandUpdateExecutorMusics(this UpdateExecutorMusicsRequest request)
        {
            return new UpdateExecutorMusicsCommand
            {
                ExecutorId = request.ExecutorId,
                MusicIds = request.MusicIds
            };
        }
    }
}