using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicMicroservice.Domain.Exceptions.ExecutorExceptions;

public class DomainExecutorException: DomainException
{
    public DomainExecutorException(string code, string message): base(code, message) {}
}