using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using MusicMicroservice.Application.Common.Interfaces.Persistance.Identity;
using MusicMicroservice.Contracts.Requests.IdentityRoles;
using MusicMicroservice.Music.API.Controllers.Common;
using RouteAttribute = Microsoft.AspNetCore.Components.RouteAttribute;

namespace MusicMicroservice.Music.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    public class AdminController: BaseController
    {
        private readonly IRoleService _roleService;

        public AdminController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpPost("assing-role")]
        public async Task<IActionResult> AssingRole([FromBody] AssignRoleRequest request)
        {
            var result = await _roleService.AssignRoleAsync(request);
            
            return HandleResult(result);
        }
        
    }
}