using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicMicroservice.Contracts.Responses.Music
{
    public record MusicResponse(Guid Id, string Name, int year, string style);
}