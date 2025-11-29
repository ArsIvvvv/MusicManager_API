using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentResults;
using MusicMicroservice.Application.Common.Interfaces.CQRS;
using MusicMicroservice.Contracts.Responses.Executer;

namespace MusicMicroservice.Application.ExecuterService.Queries
{
    public class GetExecuterByIdQuery: IQuery<Result<ExecuterResponse>>
    {
        public Guid Id { get; init; }
    }
}