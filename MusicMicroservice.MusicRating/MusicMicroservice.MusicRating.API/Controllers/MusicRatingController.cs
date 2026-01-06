using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using MusicMicroservice.MusicRating.API.Controllers.Common;
using MusicMicroservice.MusicRating.Application.Common.Interfaces.Service;
using MusicMicroservice.MusicRating.Contracts.Requests;
using MusicMicroservice.MusicRating.Contracts.Responses;
using RouteAttribute = Microsoft.AspNetCore.Components.RouteAttribute;

namespace MusicMicroservice.MusicRating.API.Controllers
{
    [Route("api/[controller]")]
    public class MusicRatingController: BaseController
    {
        private readonly IMusicRatingService _musicRatingService;

        public MusicRatingController(IMusicRatingService musicRatingService)
        {
            _musicRatingService = musicRatingService;
        }

        [HttpPost("create-music-review")]
        public async Task<IActionResult> CreateMusicReview([FromBody] CreateMusicRatingReviewRequest request)
        {
            var result = await _musicRatingService.CreateMusicRatingAsync(request);
            return HandleResult<MusicRatingReviewResponse>(result);
        }

        [HttpGet("get-rating/{id}")]
        public async Task<IActionResult> GetMusicRating([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var result = await _musicRatingService.GetMusicRatingByIdAsync(id, cancellationToken);
            return HandleResult<MusicRatingReviewResponse>(result);
        }

        [HttpGet("get-all-ratings")]
        public async Task<IActionResult> GetAllMusicRating(CancellationToken cancellationToken)
        {
            var result = await _musicRatingService.GetAllMusicRatingAsync(cancellationToken);
            return HandleResult<IEnumerable<MusicRatingReviewResponse>>(result);
        }

        [HttpGet("get-ratings-music/{musicId}")]
        public async Task<IActionResult> GetAllRatingsMusicByMusic([FromRoute] Guid musicId, CancellationToken cancellationToken)
        {
            var result = await _musicRatingService.GetAllMusicRatingByIdAsync(musicId, cancellationToken);
            return HandleResult<IEnumerable<MusicRatingReviewResponse>>(result);
        }

        [HttpDelete("delete-music-rating/{id}")]
        public async Task<IActionResult> DeleteMusicRating([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var result = await _musicRatingService.DeleteMusicRatingByIdAsync(id.ToString(), cancellationToken);
            return HandleResult(result);
        }
    }
}