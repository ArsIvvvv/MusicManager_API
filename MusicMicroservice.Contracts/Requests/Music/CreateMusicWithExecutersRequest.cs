using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MusicMicroservice.Contracts.Requests.Executer;

namespace MusicMicroservice.Contracts.Requests.Music
{
    public record CreateMusicWithExecutersRequest(string Name, int year, string style, List<CreateExecuterRequest> ExecutorIds);
}