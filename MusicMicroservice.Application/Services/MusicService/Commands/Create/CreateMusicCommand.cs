using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentResults;
using MediatR;
using MusicMicroservice.Contracts.Responses.Music;

namespace MusicMicroservice.Application.MusicService.Commands.Create;

public class CreateMusicCommand: IRequest<Result<MusicResponse>>
{
    public string? Name { get; init; }
    public int Year { get; init; }
    public string? Style { get; init; }
    public List<Guid>? ExecutorIds { get; init; }

}