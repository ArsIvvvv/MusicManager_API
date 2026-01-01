using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentResults;
using MediatR;
using MusicMicroservice.Contracts.Responses.Executor;

namespace MusicMicroservice.Application.ExecutorService.Queries
{
    public class GetExecutorByIdQuery: IRequest<Result<ExecutorResponse>>
    {
        public Guid Id { get; init; }
    }
}