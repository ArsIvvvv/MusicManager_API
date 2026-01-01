using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicMicroservice.MusicRating.Domain.Exceptions
{
    public class MongoEntityException: Exception
    {
         public MongoEntityException(string message) : base(message) { }
    }
}