using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using MusicMicroservice.Application.ExecutorService.Commands.Update;

namespace MusicMicroservice.Application.ExecutorService.Validators
{
    public class UpdateExecutorMusicsCommandValidator: AbstractValidator<UpdateExecutorMusicsCommand>
    {
        public UpdateExecutorMusicsCommandValidator()
        {
            RuleFor(e => e.ExecutorId)
            .NotEmpty()
            .WithMessage("Executor Id is required");

        RuleFor(e => e.MusicIds)
            .NotEmpty()
            .WithMessage("Musics Ids collection is required");
        }
    }
}