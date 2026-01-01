using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicMicroservice.Domain.Exceptions.MusicExceptions;

public class DomainMusicException: DomainException
{
    public DomainMusicException(string code, string message):base(code, message) {}
}