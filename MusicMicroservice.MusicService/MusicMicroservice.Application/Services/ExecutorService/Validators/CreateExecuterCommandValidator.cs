using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using MusicMicroservice.Application.ExecutorService.Commands.Create;

namespace MusicMicroservice.Application.ExecutorService.Validators
{
    public class CreateExecutorCommandValidator: AbstractValidator<CreateExecutorCommand>
    {
        public CreateExecutorCommandValidator()
        {
            RuleFor(a => a.FirstName)
                .NotEmpty()
                .WithMessage("The executor's first name must not be empty.")
                .MaximumLength(100)
                .WithMessage("The executor's first name must not be longer than 100 characters.");

            RuleFor(a => a.LastName)
                .NotEmpty()
                .WithMessage("The executor's second name must not be empty.")
                .MaximumLength(100)
                .WithMessage("The executor's second name must not be longer than 100 characters.");

            RuleFor(a => a.Nickname)    
                .NotEmpty()
                .WithMessage("The executor's nickname must not be empty.")
                .MaximumLength(100)
                .WithMessage("The executor's nickname must not be longer than 100 characters.");  
        }   
    }
}