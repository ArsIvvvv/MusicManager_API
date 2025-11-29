using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentResults;
using Microsoft.Extensions.Logging;
using MusicMicroservice.Application.Common.Errors;
using MusicMicroservice.Application.Common.Interfaces.CQRS;
using MusicMicroservice.Application.Common.Interfaces.Persistance;
using MusicMicroservice.Contracts.Responses.Executer;
using MusicMicroservice.Domain.Entities;

namespace MusicMicroservice.Application.ExecuterService.Queries
{
    public class GetAllExecuterQuery: IQuery<Result<IEnumerable<ExecuterResponse>>>
    {
        
    }
}