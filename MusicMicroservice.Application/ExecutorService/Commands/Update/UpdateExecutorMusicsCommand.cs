using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using ICommand = MusicMicroservice.Application.Common.Interfaces.CQRS.ICommand;

namespace MusicMicroservice.Application.ExecutorService.Commands.Update;

public class UpdateExecutorMusicsCommand: ICommand
{
    public Guid ExecutorId { get; init; }
    public List<Guid> MusicIds { get; init; }
}