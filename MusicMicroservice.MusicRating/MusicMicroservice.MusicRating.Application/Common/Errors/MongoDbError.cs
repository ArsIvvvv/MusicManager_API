using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MusicMicroservice.MusicRating.Application.Common.Errors.CommonErrors;

namespace MusicMicroservice.MusicRating.Application.Common.Errors
{
    public class MongoDbError: BaseError
    {
        public MongoDbError(string message) : base(message, "MONGO_DB_ERROR", 500) { }  
    }
}