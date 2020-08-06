using CNX_Domain.Entities;
using CNX_Domain.Interfaces.Application;
using CNX_Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
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

        public UserController(IUserApplication userApplication)
        {
            this._userApplication = userApplication;
        }

        [HttpGet]
        public ActionResult<IList<UserVM>> GetAllUsers()
        {
            try
            {
                var userList = this._userApplication.GetUsers();

                if (userList.Any())
                    return Ok(userList);
                else
                    return NoContent();
            }
            catch (Exception error)
            {
                return BadRequest(error.Message);
            }
        }
        [HttpGet("{id}")]
        public ActionResult<UserVM> GetUserById(string id)
        {
            try
            {
                var user = this._userApplication.GetById(id);

                if (null != user)
                    return Ok(user);

                return NotFound();
            }
            catch (Exception error)
            {
                return BadRequest(error.Message);
            }
        }

        [HttpPost]
        public ActionResult<UserVM> CreateUser(UserVM user)
        {
            try
            {
                user = this._userApplication.CreateUser(user);
                return Ok(user);
            }
            catch (Exception error)
            {
                return BadRequest(error.Message);
            }
        }
    }
}