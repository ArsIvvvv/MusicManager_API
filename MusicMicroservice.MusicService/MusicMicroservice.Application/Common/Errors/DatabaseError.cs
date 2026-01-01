using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MusicMicroservice.Application.Common.Errors.CommonErrors;

namespace MusicMicroservice.Application.Common.Errors;

public class DatabaseError: BaseError
{
    public DatabaseError(string message) 
        : base(message, 500, "DATABASE_ERROR")
    {
    }
}