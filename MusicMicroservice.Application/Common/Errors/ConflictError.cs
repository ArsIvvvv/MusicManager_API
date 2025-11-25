using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MusicMicroservice.Application.Common.Errors.CommonErrors;

namespace MusicMicroservice.Application.Common.Errors
{
    public class ConflictError: BaseError
    {
        public ConflictError(string message) 
            : base(message, 409, "CONFLICT_ERROR")
        {
        }
    }
}