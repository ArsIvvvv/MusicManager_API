using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using ICommand = MusicMicroservice.Application.Common.Interfaces.CQRS.ICommand;

namespace MusicMicroservice.Application.MusicService.Commands.Update;

public class UpdateMusicInfoCommand: ICommand
{
    public Guid MusicId { get; init; }
    public string Name { get; init; }
    public int Year { get; init; }
    public string Style { get; init; }
}