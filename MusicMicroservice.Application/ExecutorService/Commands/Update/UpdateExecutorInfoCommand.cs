using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentResults;
using MediatR;

namespace MusicMicroservice.Application.ExecutorService.Commands.Update;

public class UpdateExecutorInfoCommand: IRequest<Result>
{
    public Guid Id { get; init; }
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string Nickname { get; init; }
    
}