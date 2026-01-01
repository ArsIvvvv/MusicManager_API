using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MusicMicroservice.Contracts.Responses.Music;

namespace MusicMicroservice.Contracts.Responses.Executor;

public record ExecutorWithMusicResponse(Guid Id, string firstName, string lastName, string nickname, List<MusicResponse> Musics);