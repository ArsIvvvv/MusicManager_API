using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading.Tasks;

namespace MusicMicroservice.Domain.Common
{
    public abstract class BaseEntity<T>
    {
        public T Id {get; protected set;}
    }
}