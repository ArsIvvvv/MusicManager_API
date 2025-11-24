using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicMicroservice.Contracts.Requests.Executer
{
    public record UpdateExecuterMusicsRequest(Guid id, List<Guid> MusicIds);
}