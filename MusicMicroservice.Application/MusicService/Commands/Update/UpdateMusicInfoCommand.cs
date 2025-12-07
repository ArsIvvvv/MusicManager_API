using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using FluentResults;
using MediatR;

namespace MusicMicroservice.Application.MusicService.Commands.Update;

public class UpdateMusicInfoCommand: IRequest<Result>
{
    public Guid MusicId { get; init; }
    public string Name { get; init; }
    public int Year { get; init; }
    public string Style { get; init; }
}