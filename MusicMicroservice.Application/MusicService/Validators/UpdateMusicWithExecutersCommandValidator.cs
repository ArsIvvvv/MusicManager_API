using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using MusicMicroservice.Application.MusicService.Commands.Update;

namespace MusicMicroservice.Application.MusicService.Validators
{
    public class UpdateMusicWithExecutorsCommandValidator: AbstractValidator<UpdateMusicWithExecutorsCommand>
    {
        public UpdateMusicWithExecutorsCommandValidator()
        {
            RuleFor(x => x.MusicId)
            .NotEmpty()
            .WithMessage("Music Id is required");

            RuleFor(x => x.ExecutorId)
            .NotEmpty()
            .WithMessage("Executor Ids collection is required");
        }   
    }
}