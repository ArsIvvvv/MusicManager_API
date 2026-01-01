using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicMicroservice.Contracts.Responses.Executor;

public record ExecutorResponse(Guid Id, string firstName, string lastName, string nickname);