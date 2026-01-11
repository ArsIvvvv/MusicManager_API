using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicMicroservice.Contracts.Responses.Rating
{
    public record MusicRatingReviewResponse(
        string Id,
        Guid MusicId,
        int Rating,
        DateTime CreatedAt
    );
}