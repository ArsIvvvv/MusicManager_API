using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using FluentResults;
using MusicMicroservice.Application.Common.Interfaces.CQRS;
using ICommand = MusicMicroservice.Application.Common.Interfaces.CQRS.ICommand;

namespace MusicMicroservice.Application.MusicService.Commands.Delete;

public class DeleteMusicCommand: ICommand
{
    public Guid Id { get; init; }
}