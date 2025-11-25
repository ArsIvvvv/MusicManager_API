using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MusicMicroservice.Application.Common.Errors.CommonErrors;

namespace MusicMicroservice.Application.Common.Errors
{
    public class NotFoundError:BaseError
    {
        public NotFoundError(string message) 
            : base(message, 404, "NOT_FOUND_ERROR")
        {
        }
    }
}