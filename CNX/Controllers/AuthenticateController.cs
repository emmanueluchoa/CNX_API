using CNX_Domain.Entities;
using CNX_Domain.Interfaces.Application;
using CNX_Domain.Interfaces.Services;
using CNX_Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace CNX_API.Controllers
{
    [Route("api/Authenticate")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly IJWTApi _tokenService;
        private readonly ILogger<AuthenticateController> _logger;
        private readonly UserManager<User> _userManager;

        public AuthenticateController(IUserApplication userApplication, IJWTApi tokenService, ILogger<AuthenticateController> logger, UserManager<User> userManager)
        {
            this._tokenService = tokenService;
            this._logger = logger;
            this._userManager = userManager;
        }

        /// <summary>
        /// Provide a token to access api features.
        /// </summary>
        /// <param name="user">username, user password.</param>
        /// <returns>Token data.</returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<AuthenticatedUserVM>> Authenticate(AuthenticateUserVM user)
        {
            try
            {
                AuthenticatedUserVM authenticatedUser = await this._tokenService.GenerateApiToken(user.UserName, user.UserPassword);

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