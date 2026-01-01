using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicMicroservice.Contracts.Requests.Executor;

public record CreateExecutorRequest(string FirstName, string LastName, string Nickname);