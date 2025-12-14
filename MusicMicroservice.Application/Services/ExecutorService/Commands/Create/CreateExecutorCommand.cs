using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using FluentResults;
using MediatR;
using MusicMicroservice.Contracts.Responses.Executor;

namespace MusicMicroservice.Application.ExecutorService.Commands.Create;

public class CreateExecutorCommand: IRequest<Result<ExecutorResponse>>
{
    public string FirstName { get; init; } = string.Empty;
    public string LastName { get; init; } = string.Empty;
    public string Nickname { get; init; } = string.Empty;

}