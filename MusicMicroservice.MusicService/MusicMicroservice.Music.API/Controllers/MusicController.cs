using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MusicMicroservice.Application.MusicService.Commands.Delete;
using MusicMicroservice.Application.MusicService.Queries;
using MusicMicroservice.Application.Services.MusicService.Queries;
using MusicMicroservice.Contracts.Requests.Music;
using MusicMicroservice.Contracts.Responses.Music;
using MusicMicroservice.Music.API.Controllers.Common;
using MusicMicroservice.Music.API.Map.Music;

namespace MusicMicroservice.Music.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class MusicController: BaseController
    {
        private readonly ILogger<MusicController> _logger;
        private readonly IMediator _mediator;

        public MusicController(ILogger<MusicController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }   

        [HttpGet("give-my-info")]
        public IActionResult GetUserInfoFromToken()
        {
            _logger.LogInformation("Entry in GetUserInfoFromToken");

            var info = new
            {
                Id = User.FindFirst(ClaimTypes.NameIdentifier)?.Value,
                UserName = User.Identity?.Name,
                Email = User.FindFirst(ClaimTypes.Email)?.Value,
                DateOfBirth = User.FindFirst(ClaimTypes.DateOfBirth)?.Value,
                Roles = User.FindAll(ClaimTypes.Role).Select(c => c.Value).ToList()
            };

            return Ok(info);
        }

        [HttpGet("all-music")]
        public async Task<IActionResult> GetAllMusicsAsync(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetAllMusicQuery(), cancellationToken);

            return HandleResult(result);
        }

        [HttpGet("all-music-with-executors")]
        public async Task<IActionResult> GetAllMusicsWithExecutorsAsync(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetAllMusicWithExecutorQuery(), cancellationToken);

            return HandleResult(result);
        }

        [HttpPost("create-music")]
        [Authorize(Policy = "OlderThan18")]
        public async Task<IActionResult> CreateMusicAsync([FromBody]CreateMusicRequest request,CancellationToken cancellationToken)
        {
            var command = request.ToCommandCreateMusic();
            var result = await _mediator.Send(command, cancellationToken);

            return HandleResult(result);
        }

        [HttpPost("create-music-with-executors")]
        public async Task<IActionResult> CreateMusicWithExecutorsAsync([FromBody]CreateMusicWithExecutorsRequest request,CancellationToken cancellationToken)
        {
            var command = request.ToCommandCreateMusicWithExecutors();
            var result = await _mediator.Send(command, cancellationToken);

            return HandleResult(result);
        }

        [HttpPut("update-music")]
        public async Task<IActionResult> UpdateMusicInfoAsync([FromBody]UpdateMusicRequest request,CancellationToken cancellationToken)
        {
            var command = request.ToCommandUpdateMusicInfo();
            var result = await _mediator.Send(command, cancellationToken);

            return HandleResult(result);
        }

        [HttpPut("update-music-with-executors")]
        public async Task<IActionResult> UpdateMusicWithExecutorsAsync([FromBody]UpdateMusicWithExecutorsRequest request,CancellationToken cancellationToken)
        {
            var command = request.ToCommandUpdateMusicWithExecutors();
            var result = await _mediator.Send(command, cancellationToken);

            return HandleResult(result);
        }

        [HttpDelete("delete-music-{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteMusicsAsync([FromRoute] Guid id,CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new DeleteMusicCommand{Id = id}, cancellationToken);

            return HandleResult(result);
        }

        [HttpGet("all-music-ratings")]
        public async Task<IActionResult> GetAllMusicRatingsAsync(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetAllDetailMusicQuery(), cancellationToken);

            return HandleResult(result);
        }
        
    }
}