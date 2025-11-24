using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicMicroservice.Contracts.Requests.Music
{
    public record UpdateMusicRequest(Guid id, string Name, int year, string style);
}