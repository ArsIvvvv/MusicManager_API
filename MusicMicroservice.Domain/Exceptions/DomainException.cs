using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicMicroservice.Domain.Exceptions
{
    public class DomainException: Exception
    {
        public string Code {get;} = string.Empty;

        public DomainException(string code, string message): base(message)
        {
            Code = code;
        }
    }
}