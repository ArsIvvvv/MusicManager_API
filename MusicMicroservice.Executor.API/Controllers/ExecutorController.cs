using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MusicMicroservice.Application.ExecutorService.Commands.Delete;
using MusicMicroservice.Application.ExecutorService.Queries;
using MusicMicroservice.Contracts.Requests.Executor;
using MusicMicroservice.Executor.API.Controllers.Common;
using MusicMicroservice.Music.API.Map.Executor;

namespace MusicMicroservice.Executor.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExecutorController: BaseController
    {
        private readonly ILogger<ExecutorController> _logger;
        private readonly IMediator _mediator;

        public ExecutorController(ILogger<ExecutorController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet("all-executor")]
        public async Task<IActionResult> GetAllExecutorsAsync(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetAllExecutorQuery(), cancellationToken);

            return HandleResult(result);
        }

        [HttpGet("all-executor-with-musics")]
        public async Task<IActionResult> GetAllExecutorsWithMusicsAsync(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetAllExecutorWithMusicQuery(), cancellationToken);

            return HandleResult(result);
        }

        [HttpGet("executor-{id}")]
        public async Task<IActionResult> GetExecutorsByIdAsync([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetExecutorByIdQuery{Id = id}, cancellationToken);

            return HandleResult(result);
        }

        [HttpPost("create-executor")]
        public async Task<IActionResult> CreateMusicAsync(CreateExecutorRequest request,CancellationToken cancellationToken)
        {
            var command = request.ToCommandCreateExecutor();
            var result = await _mediator.Send(command, cancellationToken);

            return HandleResult(result);
        }

        [HttpPut("update-executor")]
        public async Task<IActionResult> UpdateExecutorAsync(UpdateExecutorRequest request,CancellationToken cancellationToken)
        {
            var command = request.ToCommandUpdateExecutorInfo();
            var result = await _mediator.Send(command, cancellationToken);

            return HandleResult(result);
        }

        [HttpPut("update-executor-with-musics")]
        public async Task<IActionResult> UpdateExecutorWithMusicsAsync(UpdateExecutorMusicsRequest request,CancellationToken cancellationToken)
        {
            var command = request.ToCommandUpdateExecutorMusics();
            var result = await _mediator.Send(command, cancellationToken);

            return HandleResult(result);
        }

        [HttpDelete("delete-executor-{id}")]
        public async Task<IActionResult> DeleteMusicsAsync([FromRoute] Guid id,CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new DeleteExecutorCommand{Id = id}, cancellationToken);

            return HandleResult(result);
        }

    }
}