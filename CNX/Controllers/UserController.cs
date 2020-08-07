using CNX_Domain.Entities.Enums;
using CNX_Domain.Interfaces.Application;
using CNX_Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

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

        [HttpGet]
        public ActionResult<IList<UserVM>> GetAllUsers()
        {
            try
            {
                var userList = this._userApplication.GetUsers();

                LogTraceVM logTrace = new LogTraceVM(string.Empty, "UserController", "GetAllUsers");
                logTrace.Parameters.Add(userList);

                if (userList.Any())
                    return Ok(userList);
                else
                    return NoContent();
            }
            catch (Exception error)
            {
                LogErrorVM logError = new LogErrorVM(error.Message, error.StackTrace, "UserController", "GetAllUsers");
                this._logger.LogError(error, logError.ToString());
                return BadRequest(error.Message);
            }
        }
        [HttpGet("{id}")]
        public ActionResult<UserVM> GetUserById(string id)
        {
            try
            {
                var user = this._userApplication.GetById(id);

                LogTraceVM logTrace = new LogTraceVM(string.Empty, "GetUserById", "GetAllUsers");
                logTrace.Parameters.Add(id);
                logTrace.Parameters.Add(user);

                if (null != user)
                    return Ok(user);

                return NotFound();
            }
            catch (Exception error)
            {
                LogErrorVM logError = new LogErrorVM(error.Message, error.StackTrace, "UserController", "GetUserById");
                logError.Parameters.Add(id);
                this._logger.LogError(error, logError.ToString());
                return BadRequest(error.Message);
            }
        }

        [HttpPost]
        public ActionResult<UserVM> CreateUser(UserVM user)
        {
            try
            {
                LogTraceVM logTrace = new LogTraceVM(string.Empty, "GetUserById", "GetAllUsers");
                logTrace.Parameters.Add(user);

                user = this._userApplication.CreateUser(user);

                return Ok(user);
            }
            catch (Exception error)
            {
                LogErrorVM logError = new LogErrorVM(error.Message, error.StackTrace, "UserController", "CreateUser");
                logError.Parameters.Add(user);
                this._logger.LogError(error, logError.ToString());
                return BadRequest(error.Message);
            }
        }
    }
}