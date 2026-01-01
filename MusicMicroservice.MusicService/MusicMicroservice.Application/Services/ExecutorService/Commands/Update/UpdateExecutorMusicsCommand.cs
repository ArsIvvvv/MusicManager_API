using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using FluentResults;
using MediatR;

namespace MusicMicroservice.Application.ExecutorService.Commands.Update;

public class UpdateExecutorMusicsCommand: IRequest<Result>
{
    public Guid ExecutorId { get; init; }
    public List<Guid> MusicIds { get; init; }
}