using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using MusicMicroservice.Application.Common.Errors;
using MusicMicroservice.Application.Common.Interfaces.Authentication;
using MusicMicroservice.Application.Common.Interfaces.Persistance.Identity;
using MusicMicroservice.Contracts.Requests.User;
using MusicMicroservice.Contracts.Responses.User;
using MusicMicroservice.Domain.Entities.Identity;

namespace MusicMicroservice.Application.Services.Identity
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signManager;

        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public UserService(UserManager<User> userManager,
         SignInManager<User> signInManager,
         IJwtTokenGenerator jwtTokenGenerator)
        {
            _userManager = userManager;
            _signManager = signInManager;
            _jwtTokenGenerator = jwtTokenGenerator;
            
        }

        public async Task<Result<UserResponse>> LoginAsync(LoginUserRequest request, CancellationToken cancellationToken = default)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if(user is null)
                return Result.Fail(new NotFoundError($"User with email {request.Email} was not found."));

            var singInResult = await _signManager.CheckPasswordSignInAsync(user, request.Password, true);
            if(!singInResult.Succeeded)
                return Result.Fail(new ValidationError("The provided credentials are invalid"));

            var token = await _jwtTokenGenerator.GenerateToken(user);

            return Result.Ok(new UserResponse(user.Email!, user.DateOfBirth, token));
        }

        public async Task<Result<IdentityResult>> RegisterAsync(RegisterUserRequest request, CancellationToken cancellationToken = default)
        {
            var newUser = new User
            {
                UserName = request.Email,
                Email = request.Email,
                DateOfBirth = request.DateOfBirth
            };

            var identityResult = await _userManager.CreateAsync(newUser, request.Password);
            if (!identityResult.Succeeded)
            {
                
                var errors = identityResult.Errors.Select(e => e.Description);
                var errorsMessage = string.Join(";",errors);

                return Result.Fail(new ValidationError(errorsMessage)); 
            }

            return Result.Ok(identityResult);
        }
    }
}