using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentResults;

namespace MusicMicroservice.MusicRating.Application.Common.Errors.CommonErrors
{
    public abstract class BaseError: Error
    {
        public int StatusCode { get; }
        public string ErrorCode { get; } = string.Empty;

        protected BaseError(string message, string errorCode, int statusCode) : base(message)
        {
            StatusCode = statusCode;
            ErrorCode = errorCode;
            Metadata.Add("Timestamp", DateTime.UtcNow);
        }
    }
}