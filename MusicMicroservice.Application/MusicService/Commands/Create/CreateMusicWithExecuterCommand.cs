using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MusicMicroservice.Application.Common.Interfaces.CQRS;
using MusicMicroservice.Contracts.Requests.Executer;
using MusicMicroservice.Contracts.Requests.Music;
using MusicMicroservice.Contracts.Responses.Music;

namespace MusicMicroservice.Application.MusicService.Commands.Create;

public class CreateMusicWithExecuterCommand: ICommand<MusicWithExecutersResponse>
{
    public string Name { get; init; }
    public int Year { get; init; }
    public string Style { get; init; }
    public List<CreateExecuterRequest> Executers { get; init; }
}