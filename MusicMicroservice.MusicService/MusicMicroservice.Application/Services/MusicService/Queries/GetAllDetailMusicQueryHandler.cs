using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentResults;
using MediatR;
using Microsoft.Extensions.Logging;
using MusicMicroservice.Application.Common.Errors;
using MusicMicroservice.Application.Common.Interfaces.HttpService;
using MusicMicroservice.Application.Common.Interfaces.Persistance;
using MusicMicroservice.Application.Common.Interfaces.Persistance.Redis;
using MusicMicroservice.Contracts.Responses.MusicRatingDetails;

namespace MusicMicroservice.Application.Services.MusicService.Queries
{
    public class GetAllDetailMusicQueryHandler : IRequestHandler<GetAllDetailMusicQuery, Result<MusicRatingDetailsResponse>>
    {
        private readonly IMusicRepository _musicRepository;
        private readonly IMusicRatingHttpService _musicRatingHttpService;
        private readonly ILogger<GetAllDetailMusicQueryHandler> _logger;
        private readonly ICacheService _cacheService;

        public GetAllDetailMusicQueryHandler(
            IMusicRepository musicRepository,
            IMusicRatingHttpService musicRatingHttpService,
            ICacheService cacheService,
            ILogger<GetAllDetailMusicQueryHandler> logger)
        {
            _musicRepository = musicRepository;
            _musicRatingHttpService = musicRatingHttpService;
            _cacheService = cacheService;
            _logger = logger;
        }

        public async Task<Result<MusicRatingDetailsResponse>> Handle(GetAllDetailMusicQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var ratings = await _musicRatingHttpService.GetAllRatingsAsync(cancellationToken);
                if (!ratings.IsSuccess)
                {
                    return Result.Fail(new NotFoundError("Ratings not found"));
                }

                var countRatings = await _cacheService.GetAsync<string>("RatingCount");
                if(countRatings is null)
                {
                    countRatings = "Not found count ratings in cache";
                }

                return Result.Ok(new MusicRatingDetailsResponse(ratings.Value, countRatings));
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Unexpected error get all detail music");
                return Result.Fail<MusicRatingDetailsResponse>(ex.Message);
            }
        }
    }
}