using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MusicMicroservice.Application.Common.Interfaces.CQRS;
using MusicMicroservice.Contracts.Responses.Music;

namespace MusicMicroservice.Application.MusicService.Commands.Create;

public class CreateMusicCommand: ICommand<MusicResponse>
{
    public string Name { get; init; }
    public int Year { get; init; }
    public string Style { get; init; }
    public List<Guid> ExecuterIds { get; init; }

}