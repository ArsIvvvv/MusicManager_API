using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using ICommand = MusicMicroservice.Application.Common.Interfaces.CQRS.ICommand;

namespace MusicMicroservice.Application.ExecuterService.Commands.Update;

public class UpdateExecuterMusicsCommand: ICommand
{
    public Guid ExecuterId { get; init; }
    public List<Guid> MusicIds { get; init; }
}