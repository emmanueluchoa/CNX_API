using CNX_Domain.Interfaces.Application;
using CNX_Domain.Interfaces.Services;
using CNX_Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace CNX_API.Controllers
{
    [Route("api/Authenticate")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly IJWTApi _tokenService;
        private readonly ILogger<AuthenticateController> _logger;

        public AuthenticateController(IUserApplication userApplication, IJWTApi tokenService, ILogger<AuthenticateController> logger)
        {
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

                LogTraceVM logTrace = new LogTraceVM(string.Empty, "AuthenticateController", "Authenticate");
                logTrace.Parameters.Add(user);

                return Ok(authenticatedUser);
            }
            catch (Exception error)
            {
                LogErrorVM logError = new LogErrorVM(error.Message, error.StackTrace, "AuthenticateController", "Authenticate");
                logError.Parameters.Add(user);
                this._logger.LogError(error, logError.ToString());
                return BadRequest(error.Message);
            }
        }
    }
}