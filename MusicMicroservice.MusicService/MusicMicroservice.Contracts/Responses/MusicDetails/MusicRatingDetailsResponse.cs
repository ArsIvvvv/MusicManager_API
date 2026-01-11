using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MusicMicroservice.Contracts.Responses.Rating;

namespace MusicMicroservice.Contracts.Responses.MusicRatingDetails
{
    public record MusicRatingDetailsResponse(
        IEnumerable<MusicRatingReviewResponse> ratings,
        string countRatings);
}