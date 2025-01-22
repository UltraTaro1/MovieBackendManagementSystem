using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MovieBackendManagementSystem.Api.Model;
using MovieBackendManagementSystem.Api.Services.IServices;

namespace MovieBackendManagementSystem.Api.Controlllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegisterController : ControllerBase
    {
        private readonly IUserService _userService;

        public RegisterController(IUserService userService)
        {
            this._userService = userService;
        }

        [HttpPost]
        public string Register(User user)
        {
            return _userService.Insert(user);
        }
    }
}