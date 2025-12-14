using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using MusicMicroservice.Application.MusicService.Commands.Create;

namespace MusicMicroservice.Application.MusicService.Validators
{
    public class CreateMusicWithExecutorsCommandValidator:AbstractValidator<CreateMusicWithExecutorCommand>
    {
        public CreateMusicWithExecutorsCommandValidator()
        {
             RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name do not empty")
            .MaximumLength(100);

            RuleFor(x => x.Year)
            .GreaterThan(1950)
            .WithMessage("Year must be greater than 1950.");

            RuleFor(x => x.Style)
            .NotEmpty().WithMessage("Style do not empty");
        }
    }
}