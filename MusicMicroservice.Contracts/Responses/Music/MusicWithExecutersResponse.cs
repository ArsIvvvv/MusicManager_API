using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MusicMicroservice.Contracts.Responses.Executer;

namespace MusicMicroservice.Contracts.Responses.Music
{
    public record MusicWithExecutersResponse(Guid Id, string Name, int year, string style, List<ExecuterResponse> Executers);
}