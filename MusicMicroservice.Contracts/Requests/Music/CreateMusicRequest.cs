using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicMicroservice.Contracts.Requests.Music
{
     public record CreateMusicRequest(string Name, int year, string style, List<Guid> ExecutorIds);
}