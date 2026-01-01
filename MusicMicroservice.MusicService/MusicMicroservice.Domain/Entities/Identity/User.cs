using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace MusicMicroservice.Domain.Entities.Identity
{
    public class User: IdentityUser
    {
        public DateOnly DateOfBirth {get; set; } 

    }
}