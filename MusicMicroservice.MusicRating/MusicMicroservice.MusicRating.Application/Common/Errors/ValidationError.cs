using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MusicMicroservice.MusicRating.Application.Common.Errors.CommonErrors;

namespace MusicMicroservice.MusicRating.Application.Common.Errors
{
    public class ValidationError: BaseError
    {
        public ValidationError(string message) : base(message, "VALIDATION_ERROR", 400) { }
    }
}