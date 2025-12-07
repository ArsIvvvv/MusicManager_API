using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentResults;
using Microsoft.Extensions.Logging;
using MusicMicroservice.Application.Common.Errors;
using MediatR;
using MusicMicroservice.Application.Common.Interfaces.Persistance;
using MusicMicroservice.Contracts.Responses.Executor;
using MusicMicroservice.Domain.Entities;

namespace MusicMicroservice.Application.ExecutorService.Queries
{
    public class GetAllExecutorQuery: IRequest<Result<IEnumerable<ExecutorResponse>>>
    {
        
    }
}