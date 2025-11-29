using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using FluentResults;
using MusicMicroservice.Application.Common.Interfaces.CQRS;
using MusicMicroservice.Contracts.Responses.Executer;

namespace MusicMicroservice.Application.ExecuterService.Commands.Create;

public class CreateExecuterCommand: ICommand<ExecuterResponse>
{
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string Nickname { get; init; }

}