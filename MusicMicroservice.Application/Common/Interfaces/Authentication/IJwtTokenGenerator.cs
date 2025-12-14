using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MusicMicroservice.Domain.Entities.Identity;

namespace MusicMicroservice.Application.Common.Interfaces.Authentication
{
    public interface IJwtTokenGenerator
    {
        Task<string> GenerateToken(User user);
    }
}