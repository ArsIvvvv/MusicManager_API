using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicMicroservice.Contracts.Requests.User
{
    public record LoginUserRequest(string Email, string Password);
}