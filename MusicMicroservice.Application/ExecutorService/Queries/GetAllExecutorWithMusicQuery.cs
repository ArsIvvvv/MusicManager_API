using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentResults;
using MusicMicroservice.Application.Common.Interfaces.CQRS;
using MusicMicroservice.Contracts.Responses.Executor;

namespace MusicMicroservice.Application.ExecutorService.Queries
{
    public class GetAllExecutorWithMusicQuery: IQuery<Result<IEnumerable<ExecutorWithMusicResponse>>>
    {
        
    }
}