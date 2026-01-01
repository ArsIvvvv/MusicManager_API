using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentResults;
using MediatR;
using MusicMicroservice.Contracts.Responses.Music;
using MusicMicroservice.Domain.Entities;

namespace MusicMicroservice.Application.MusicService.Queries
{
    public class GetAllMusicWithExecutorQuery: IRequest<Result<IEnumerable<MusicWithExecutorsResponse>>>
    {
        
    }
}