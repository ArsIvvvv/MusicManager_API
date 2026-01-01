using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MusicMicroservice.Application.Common.Interfaces.Persistance.Identity;
using MusicMicroservice.Contracts.Requests.User;
using MusicMicroservice.Contracts.Responses.User;
using MusicMicroservice.Music.API.Controllers.Common;



namespace MusicMicroservice.Music.API.Controllers
{
    [Route("api/[controller]")]
    public class AuthController: BaseController
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterUserRequest request)
        {
            var result = await _userService.RegisterAsync(request);

            return HandleResult<IdentityResult>(result); 
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginUser([FromBody] LoginUserRequest request)
        {
            var result = await _userService.LoginAsync(request);

            return HandleResult<UserResponse>(result); 
        }
    }
}