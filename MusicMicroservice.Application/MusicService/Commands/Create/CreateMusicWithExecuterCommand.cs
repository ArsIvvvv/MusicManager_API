using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MusicMicroservice.Application.Common.Interfaces.CQRS;
using MusicMicroservice.Contracts.Requests.Executor;
using MusicMicroservice.Contracts.Requests.Music;
using MusicMicroservice.Contracts.Responses.Music;

namespace MusicMicroservice.Application.MusicService.Commands.Create;

public class CreateMusicWithExecutorCommand: ICommand<MusicWithExecutorsResponse>
{
    public string Name { get; init; }
    public int Year { get; init; }
    public string Style { get; init; }
    public List<CreateExecutorRequest> Executors { get; init; }
}