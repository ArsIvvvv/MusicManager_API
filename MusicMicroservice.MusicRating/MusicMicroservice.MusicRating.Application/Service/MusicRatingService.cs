using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentResults;
using Microsoft.Extensions.Logging;
using MusicMicroservice.MusicRating.Application.Common.Errors;
using MusicMicroservice.MusicRating.Application.Common.Interfaces.Persistence;
using MusicMicroservice.MusicRating.Application.Common.Interfaces.Service;
using MusicMicroservice.MusicRating.Contracts.Requests;
using MusicMicroservice.MusicRating.Contracts.Responses;
using MusicMicroservice.MusicRating.Domain.Exceptions;
using MusicMicroservice.MusicRating.Domain.MongoEntities;

namespace MusicMicroservice.MusicRating.Application.Service
{
    public class MusicRatingService : IMusicRatingService
    {
        private readonly IMusicRatingRepository _musicRatingRepository;

        private readonly ILogger<MusicRatingService> _logger;

        public MusicRatingService(
            IMusicRatingRepository musicRatingRepository,
            ILogger<MusicRatingService> logger)
        {
            _musicRatingRepository = musicRatingRepository;
            _logger = logger;
        }

        public async Task<Result<MusicRatingReviewResponse>> CreateMusicRatingAsync(CreateMusicRatingReviewRequest request, CancellationToken cancellationToken = default)
        {
            try
            {
                var newMusicRating = MusicRatingReview.Create(request.musicId, request.rating);

                await _musicRatingRepository.AddAsync(newMusicRating, cancellationToken);

                return Result.Ok(MapToResponse(newMusicRating));
            }
            catch(MongoEntityException mongoEx)
            {
                return Result.Fail(new ValidationError(mongoEx.Message));
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Result.Fail(new MongoDbError("MongoDB error occurred while creating a music rating review."));
            }
        }

        public async Task<Result> DeleteMusicRatingByIdAsync(string id, CancellationToken cancellationToken = default)
        {
            try{
                var result = await _musicRatingRepository.DeleteByIdAsync(id, cancellationToken);
                if(!result)
                {
                    return Result.Fail(new NotFoundError($"Music rating review with id {id} not found."));
                }

                return Result.Ok();
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Result.Fail(new MongoDbError("MongoDB error occurred while deleting a music rating review."));
            }
        }

        public async Task<Result<IEnumerable<MusicRatingReviewResponse>>> GetAllMusicRatingByIdAsync(Guid musicId, CancellationToken cancellationToken = default)
        {
            try
            {
                var ratings = await _musicRatingRepository.GetByMusicIdAsync(musicId, cancellationToken);
                if(ratings.Count() < 1)
                {
                    return Result.Fail(new NotFoundError($"Music rating reviews for music with id {musicId} not found."));
                }

                return Result.Ok(ratings.Select(r => MapToResponse(r)));
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Result.Fail(new MongoDbError("MongoDB error occurred while retrieving music rating reviews by music id."));
            }
        }

        public async Task<Result<IEnumerable<MusicRatingReviewResponse>>> GetAllMusicRatingAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                var ratings = await _musicRatingRepository.GetAllAsync(cancellationToken);

                 return Result.Ok(ratings.Select(r => MapToResponse(r)));
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Result.Fail(new MongoDbError("MongoDB error occurred while retrieving all music rating reviews."));
            }
        }

        public async Task<Result<MusicRatingReviewResponse>> GetMusicRatingByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            try
            {
                var rating = await _musicRatingRepository.GetByIdAsync(id, cancellationToken);
                if(rating == null)
                {
                    return Result.Fail(new NotFoundError($"Music rating review with id {id} not found."));
                }

                return Result.Ok(MapToResponse(rating));
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Result.Fail(new MongoDbError("MongoDB error occurred while retrieving a music rating review by id."));
            }
        }


        private MusicRatingReviewResponse MapToResponse(MusicRatingReview review)
        {
            return new MusicRatingReviewResponse(
                review.Id.ToString(), 
                review.MusicId, 
                review.Rating, 
                review.CreatedAt
            );
        }
    }
}