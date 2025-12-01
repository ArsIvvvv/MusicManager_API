using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using MusicMicroservice.Application.ExecutorService.Commands.Update;
using MusicMicroservice.Application.MusicService.Commands.Update;

namespace MusicMicroservice.Application.MusicService.Validators
{
    public class UpdateMusicInfoCommandValidator:AbstractValidator<UpdateMusicInfoCommand>
    {
        public UpdateMusicInfoCommandValidator()
        {
            RuleFor(x => x.MusicId)
            .NotEmpty().WithMessage("Music Id is required");

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