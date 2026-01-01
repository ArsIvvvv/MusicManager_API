using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicMicroservice.Contracts.Requests.Music;

public record UpdateMusicWithExecutorsRequest(Guid MusicId, List<Guid> ExecutorIds);