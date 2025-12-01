using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MusicMicroservice.Application.Common.Interfaces.CQRS;

namespace MusicMicroservice.Application.ExecutorService.Commands.Update;

public class UpdateExecutorInfoCommand: ICommand
{
    public Guid ExecutorId { get; init; }
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string Nickname { get; init; }
    
}