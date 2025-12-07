using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentResults;
using MediatR;
using MusicMicroservice.Contracts.Responses.Music;

namespace MusicMicroservice.Application.MusicService.Queries;

public class GetAllMusicQuery: IRequest<Result<IEnumerable<MusicResponse>>>
{
    
}