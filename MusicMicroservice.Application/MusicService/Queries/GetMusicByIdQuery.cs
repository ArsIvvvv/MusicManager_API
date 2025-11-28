using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentResults;
using MusicMicroservice.Application.Common.Interfaces.CQRS;
using MusicMicroservice.Contracts.Responses.Music;

namespace MusicMicroservice.Application.MusicService.Queries;

public class GetMusicByIdQuery: IQuery<Result<MusicResponse>>
{
    public Guid Id { get; init; }
}