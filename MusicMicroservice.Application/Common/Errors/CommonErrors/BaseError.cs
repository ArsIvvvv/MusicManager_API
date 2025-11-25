using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentResults;

namespace MusicMicroservice.Application.Common.Errors.CommonErrors
{
    public abstract class BaseError: Error
    {
        public int StatusCode { get; }
        public string ErrorCode { get; }  =  string.Empty;

        protected BaseError(string message, int statusCode, string errorCode) : base(message)
        {
            StatusCode = statusCode;
            ErrorCode = errorCode;
        }   
    }
}