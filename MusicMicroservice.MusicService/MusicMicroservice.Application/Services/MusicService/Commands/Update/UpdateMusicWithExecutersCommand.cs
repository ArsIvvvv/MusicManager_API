using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using FluentResults;
using MediatR;

namespace MusicMicroservice.Application.MusicService.Commands.Update;

public class UpdateMusicWithExecutorsCommand: IRequest<Result>
{
    public Guid MusicId { get; init; }
    public List<Guid> ExecutorIds { get; init; }
}