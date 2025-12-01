using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using MusicMicroservice.Application.ExecutorService.Commands.Update;

namespace MusicMicroservice.Application.ExecutorService.Validators
{
    public class UpdateExecutorInfoCommandValidator: AbstractValidator<UpdateExecutorInfoCommand>
    {
        public UpdateExecutorInfoCommandValidator()
        {
            RuleFor(e =>e.ExecutorId)
            .NotEmpty()
            .WithMessage("The executor's Id must not be empty.");

            RuleFor(e => e.FirstName)
                .NotEmpty()
                .WithMessage("The executor's first name must not be empty.")
                .MaximumLength(100)
                .WithMessage("The executor's first name must not be longer than 100 characters.");

            RuleFor(e => e.LastName)
                .NotEmpty()
                .WithMessage("The executor's second name must not be empty.")
                .MaximumLength(100)
                .WithMessage("The executor's second name must not be longer than 100 characters.");

            RuleFor(e => e.Nickname)    
                .NotEmpty()
                .WithMessage("The executor's nickname must not be empty.")
                .MaximumLength(100)
                .WithMessage("The executor's nickname must not be longer than 100 characters.");  
        }
    }
}