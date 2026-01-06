using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MusicMicroservice.MusicRating.Application.Common.Errors.CommonErrors;

namespace MusicMicroservice.MusicRating.Application.Common.Errors
{
    public class NotFoundError: BaseError
    {
        public NotFoundError(string message) : base(message, "NOT_FOUND_ERROR", 404) { }
    }
}