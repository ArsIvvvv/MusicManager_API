using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;

namespace MusicMicroservice.Contracts.Responses.User
{
    public record UserResponse(string Email, DateOnly DateOfBirth, string Token);
}