using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentResults;
using Microsoft.AspNetCore.Mvc;
using MusicMicroservice.Application.Common.Errors;

namespace MusicMicroservice.Executor.API.Controllers.Common
{
    public class BaseController: ControllerBase
    {
     protected IActionResult HandleResult<T>(Result<T> result)
    {
        if (result.IsSuccess)
            return Ok(result.Value);

        return ToActionResult(result.Errors[0]);
    }

    protected IActionResult HandleResult(Result result)
    {
        if (result.IsSuccess)
            return Ok();

        return ToActionResult(result.Errors[0]);
    }

    private IActionResult ToActionResult(IError error)
    {
        return error switch
        {
            ValidationError validationError =>
                CreateProblemDetailsResult(validationError, validationError.StatusCode, validationError.Message, validationError.ErrorCode),

            NotFoundError notFoundError =>
                CreateProblemDetailsResult(notFoundError, notFoundError.StatusCode, notFoundError.Message, notFoundError.ErrorCode),

            ConflictError conflictError =>
                CreateProblemDetailsResult(conflictError, conflictError.StatusCode, conflictError.Message, conflictError.ErrorCode),

            DatabaseError databaseError =>
                CreateProblemDetailsResult(databaseError, databaseError.StatusCode, databaseError.Message, databaseError.ErrorCode),

            _ => CreateProblemDetailsResult(new Error(""), StatusCodes.Status500InternalServerError, error.Message, "")
        };
    }

    private IActionResult CreateProblemDetailsResult(Error error, int statusCode, string title, string code)
    {
        var problemDetails = new ProblemDetails
        {
            Status = statusCode,
            Title = title,
            Detail = code
        };

        if(error.Metadata?.Count > 0)
        {
            problemDetails.Extensions["errors"] = error.Metadata;
        }

        return new ObjectResult(problemDetails)
        {
            StatusCode = statusCode
        };
    }

    }
}