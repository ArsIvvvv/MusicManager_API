using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MusicMicroservice.Contracts.Requests.Executor;

namespace MusicMicroservice.Contracts.Requests.Music;

public record CreateMusicWithExecutorsRequest(string Name, int year, string style, List<CreateExecutorRequest> Executors);