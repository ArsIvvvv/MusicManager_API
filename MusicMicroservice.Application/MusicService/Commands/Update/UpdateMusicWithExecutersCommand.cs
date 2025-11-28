using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using ICommand = MusicMicroservice.Application.Common.Interfaces.CQRS.ICommand;

namespace MusicMicroservice.Application.MusicService.Commands.Update;

public class UpdateMusicWithExecutersCommand: ICommand
{
    public Guid MusicId { get; init; }
    public List<Guid> ExecuterId { get; init; }
}