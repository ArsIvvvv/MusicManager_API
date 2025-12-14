using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentResults;
using Microsoft.AspNetCore.Identity;
using MusicMicroservice.Contracts.Requests.User;
using MusicMicroservice.Contracts.Responses.User;

namespace MusicMicroservice.Application.Common.Interfaces.Persistance.Identity
{
    public interface IUserService
    {
        Task<Result<IdentityResult>> RegisterAsync(RegisterUserRequest request, CancellationToken cancellationToken = default);
        Task<Result<UserResponse>> LoginAsync(LoginUserRequest request, CancellationToken cancellationToken = default);
    }
}