using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using FluentResults;
using MusicMicroservice.Application.Common.Interfaces.CQRS;
using MusicMicroservice.Contracts.Responses.Executor;

namespace MusicMicroservice.Application.ExecutorService.Commands.Create;

public class CreateExecutorCommand: ICommand<ExecutorResponse>
{
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string Nickname { get; init; }

}