using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using ICommand = MusicMicroservice.Application.Common.Interfaces.CQRS.ICommand;

namespace MusicMicroservice.Application.ExecuterService.Commands.Delete;

public class DeleteExecuterCommand: ICommand
{
    public Guid Id { get; init; }
}