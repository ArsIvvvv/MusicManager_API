using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MusicMicroservice.Contracts.Responses.Executor;

namespace MusicMicroservice.Contracts.Responses.Music;

public record MusicWithExecutorsResponse(Guid id, string name, int year, string style, List<ExecutorResponse> Executors);