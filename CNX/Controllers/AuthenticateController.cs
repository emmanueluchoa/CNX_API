using CNX_Domain.Interfaces.Application;
using CNX_Domain.Interfaces.Services;
using CNX_Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;

namespace CNX_API.Controllers
{
    [Route("api/Authenticate")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly IUserApplication _userApplication;
        private readonly IJWTApi _tokenService;
        private readonly ILogger<AuthenticateController> _logger;

        public AuthenticateController(IUserApplication userApplication, IJWTApi tokenService, ILogger<AuthenticateController> logger)
        {
            this._userApplication = userApplication;
            this._tokenService = tokenService;
            this._logger = logger;
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
                this._logger.LogError(error, error.Message, user);
                return BadRequest(error.Message);
            }

        }
    }
}