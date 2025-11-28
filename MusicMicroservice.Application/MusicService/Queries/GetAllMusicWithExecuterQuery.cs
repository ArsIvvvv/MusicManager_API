using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentResults;
using MusicMicroservice.Application.Common.Interfaces.CQRS;
using MusicMicroservice.Contracts.Responses.Music;
using MusicMicroservice.Domain.Entities;

namespace MusicMicroservice.Application.MusicService.Queries
{
    public class GetAllMusicWithExecuterQuery: IQuery<Result<IEnumerable<MusicWithExecutersResponse>>>
    {
        
    }
}