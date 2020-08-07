using CNX_Domain.Interfaces.Application;
using CNX_Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CNX_API.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UserController : ControllerBase
    {
        private readonly IUserApplication _userApplication;
        private readonly ILogger<UserController> _logger;

        public UserController(IUserApplication userApplication, ILogger<UserController> logger)
        {
            this._userApplication = userApplication;
            this._logger = logger;
        }

        /// <summary>
        /// Create a new user.
        /// </summary>
        /// <param name="user">User data</param>
        /// <returns>Message with sucess or error.</returns>
        [HttpPost]
        [Authorize]
        public async Task<ActionResult> CreateUser(CreateUserVM user)
        {
            try
            {
                LogTraceVM logTrace = new LogTraceVM(string.Empty, "GetUserById", "GetAllUsers");
                logTrace.Parameters.Add(user);

                var retorno = await this._userApplication.CreateUser(user);

                return Ok($"User {user.UserName} successfully created!");
            }
            catch (Exception error)
            {
                LogErrorVM logError = new LogErrorVM(error.Message, error.StackTrace, "UserController", "CreateUser");
                logError.Parameters.Add(user);
                this._logger.LogError(error, logError.ToString());
                return BadRequest(error.Message);
            }
        }

        /// <summary>
        /// Reset user password.
        /// </summary>
        /// <param name="reset"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        [Route("reset")]
        public async Task<ActionResult> ResetPasswordAsync(ResetPasswordUserVM reset)
        {
            try
            {
                LogTraceVM logTrace = new LogTraceVM(string.Empty, "GetUserById", "GetAllUsers");
                logTrace.Parameters.Add(reset);

                if (ModelState.IsValid)
                    await this._userApplication.ResetPasswordAsync(reset);
                else
                    throw new Exception(string.Join(",", ModelState.Values.Where(value => value.Errors.Any()).Select(value => value.Errors.Select(error => error.ErrorMessage))));

                return Ok("Your password has been reseted!");
            }
            catch (Exception error)
            {
                LogErrorVM logError = new LogErrorVM(error.Message, error.StackTrace, "UserController", "CreateUser");
                logError.Parameters.Add(reset);
                this._logger.LogError(error, logError.ToString());
                return BadRequest(error.Message);
            }
        }
    }
}