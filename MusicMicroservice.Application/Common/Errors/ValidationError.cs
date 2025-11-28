using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MusicMicroservice.Application.Common.Errors.CommonErrors;

namespace MusicMicroservice.Application.Common.Errors;

public class ValidationError: BaseError
{
    public ValidationError(string message) 
        : base(message, 400, "VALIDATION_ERROR")
    {
    }
}