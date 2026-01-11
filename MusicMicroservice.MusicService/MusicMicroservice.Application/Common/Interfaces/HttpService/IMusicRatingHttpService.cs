using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentResults;
using MusicMicroservice.Contracts.Responses.Rating;

namespace MusicMicroservice.Application.Common.Interfaces.HttpService
{
    public interface IMusicRatingHttpService
    {
        Task<Result<IEnumerable<MusicRatingReviewResponse>>> GetAllRatingsAsync(CancellationToken cancellationToken = default);
    }
}