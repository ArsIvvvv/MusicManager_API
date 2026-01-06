using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicMicroservice.MusicRating.Contracts.Requests
{
    public record CreateMusicRatingReviewRequest(Guid musicId, int rating);
}