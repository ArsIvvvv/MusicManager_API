using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentResults;
using MusicMicroservice.MusicRating.Contracts.Requests;
using MusicMicroservice.MusicRating.Contracts.Responses;

namespace MusicMicroservice.MusicRating.Application.Common.Interfaces.Service
{
    public interface IMusicRatingService
    {
        Task<Result<MusicRatingReviewResponse>> CreateMusicRatingAsync(CreateMusicRatingReviewRequest request, CancellationToken cancellationToken = default);

        Task<Result<MusicRatingReviewResponse>> GetMusicRatingByIdAsync(Guid id, CancellationToken cancellationToken = default);

        Task<Result<IEnumerable<MusicRatingReviewResponse>>> GetAllMusicRatingAsync(CancellationToken cancellationToken = default);

        Task<Result<IEnumerable<MusicRatingReviewResponse>>> GetAllMusicRatingByIdAsync(Guid musicId, CancellationToken cancellationToken = default);

        Task<Result> DeleteMusicRatingByIdAsync(string id, CancellationToken cancellationToken = default);
    }
}