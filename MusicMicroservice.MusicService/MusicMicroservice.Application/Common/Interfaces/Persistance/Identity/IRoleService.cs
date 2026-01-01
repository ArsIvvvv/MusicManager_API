using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentResults;
using MusicMicroservice.Contracts.Requests.IdentityRoles;

namespace MusicMicroservice.Application.Common.Interfaces.Persistance.Identity
{
    public interface IRoleService
    {
        Task<Result> AssignRoleAsync(AssignRoleRequest request);
    }
}