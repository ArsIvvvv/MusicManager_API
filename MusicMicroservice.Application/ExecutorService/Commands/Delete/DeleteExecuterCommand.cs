using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using FluentResults;
using MediatR;

namespace MusicMicroservice.Application.ExecutorService.Commands.Delete;

public class DeleteExecutorCommand: IRequest<Result>
{
    public Guid Id { get; init; }
}