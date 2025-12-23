using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace MusicMicroservice.Application.Authorization.Requirements
{
    public class AgeRequirements: IAuthorizationRequirement
    {
        public int MinAge {get;}

        public AgeRequirements(int minAge)
        {
            MinAge = minAge;
        }
        
    }
}