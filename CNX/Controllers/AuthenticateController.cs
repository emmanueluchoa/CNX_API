using CNX_Domain.Interfaces.Application;
using CNX_Domain.Interfaces.Services;
using CNX_Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace CNX_API.Controllers
{
    [Route("api/Authenticate")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly IUserApplication _userApplication;
        private readonly IJWTApi _tokenService;

        public AuthenticateController(IUserApplication userApplication, IJWTApi tokenService)
        {
            this._userApplication = userApplication;
            this._tokenService = tokenService;
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult<dynamic> Authenticate(UserFullVM user)
        {
            try
            {
                AuthenticatedUserVM authenticatedUser = this._tokenService.GenerateApiToken(user.UserName, user.UserPassword);
                return Ok(authenticatedUser);
            }
            catch (Exception error)
            {
                return BadRequest(error.Message);
            }

        }
    }
}