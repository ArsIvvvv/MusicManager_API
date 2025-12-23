using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicMicroservice.Contracts.Requests.IdentityRoles
{
    public record AssignRoleRequest(string Email, string Role);   
}