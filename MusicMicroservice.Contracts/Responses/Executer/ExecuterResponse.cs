using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicMicroservice.Contracts.Responses.Executer
{
    public record ExecuterResponse(Guid Id, string firstName, string lastName, string nickname);
}