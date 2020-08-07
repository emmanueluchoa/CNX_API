using AutoMapper;
using CNX_Domain.Entities;
using CNX_Domain.Interfaces.Application;
using CNX_Domain.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CNX_Domain.Application
{
    public class UserApplication : IUserApplication
    {
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;

        public UserApplication(IMapper mapper, UserManager<User> userManager)
        {
            this._mapper = mapper;
            this._userManager = userManager;
        }

        public async Task<UserVM> CreateUser(CreateUserVM userVM)
        {
            UserFullVM userFull = this._mapper.Map<UserFullVM>(userVM);
            if (userFull.IsValid())
            {
                User user = this._mapper.Map<User>(userFull);
                var result = await this._userManager.CreateAsync(user, userVM.UserPassword);

                return this._mapper.Map<UserVM>(user);
            }

            throw new Exception(userFull.GetValidationErrors());
        }

        public UserVM GetById(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<UserVM> GetUser(string userName, string userPassword)
        {
            var user = await this._userManager.FindByNameAsync(userName);

            if (null == user || !await this._userManager.CheckPasswordAsync(user, userPassword))
                throw new Exception("Invalid user!");

            return this._mapper.Map<UserVM>(user);
        }

        public IList<UserVM> GetUsers()
        {
            throw new NotImplementedException();
        }

        public UserVM UpdateUser(UserVM userVM)
        {
            throw new NotImplementedException();
        }

        public bool ValidateIfUserExists(string userName, string userPassword)
        {
            throw new NotImplementedException();
        }

        public async Task ResetPasswordAsync(ResetPasswordUserVM resetPasswordUserVM)
        {
            User user = await this._userManager.FindByNameAsync(resetPasswordUserVM.UserName);

            if (null != user)
            {
                string resetPasswordToken = await this._userManager.GeneratePasswordResetTokenAsync(user);
                await this._userManager.ResetPasswordAsync(user, resetPasswordToken, resetPasswordUserVM.NewPassword);
            }
            else
                throw new Exception("User not found or invalid.");
        }

        //public IList<UserVM> GetUsers() =>
        //    this._mapper.Map<IList<UserVM>>(this._userRepository.GetAll());

        //public UserVM GetById(string id) =>
        //    this._mapper.Map<UserVM>(this._userRepository.GetById(id));

        //public UserVM CreateUser(UserVM userVM)
        //{
        //    var user = this._mapper.Map<User>(userVM);

        //    //if (user.IsValid())
        //    //{
        //    //    if (ValidateIfUserExists(user.UserName, user.UserPassword))
        //    //        throw new Exception("User already exists.");

        //    //    //user = this._userRepository.Insert(user);
        //    //    userVM = this._mapper.Map<UserVM>(user);
        //    //    return userVM;
        //    //}
        //    //else
        //    //    throw new Exception(user.GetValidationErrors());
        //}

        //public bool ValidateIfUserExists(string userName, string userPassword) =>
        //   this._userRepository.HasAny(param => param.UserName.Equals(userName) && param.UserPassword.Equals(userPassword));

        //public UserVM UpdateUser(UserVM userVM)
        //{
        //    var user = this._mapper.Map<Users>(userVM);

        //    if (user.IsValid())
        //    {
        //        var teste = this._userRepository.Update(user);
        //        return this._mapper.Map<UserVM>(user);
        //    }
        //    else
        //        throw new Exception(user.GetValidationErrors());
        //}

        //public UserVM GetUser(string userName, string userPassword)
        //{
        //    CheckUserName(userName);
        //    CheckUserPassword(userPassword);

        //    Users user = this._userRepository.GetFirst(user => user.UserName.Equals(userName) && user.UserPassword.Equals(userPassword));
        //    if (null != user)
        //        return this._mapper.Map<UserVM>(user);
        //    else
        //        throw new Exception("User not found.");
        //}

        //private static void CheckUserPassword(string userPassword)
        //{
        //    if (string.IsNullOrWhiteSpace(userPassword))
        //        throw new Exception("User password not provided!");
        //}

        //private static void CheckUserName(string userName)
        //{
        //    if (string.IsNullOrWhiteSpace(userName))
        //        throw new Exception("User name not provided.");
        //}
    }
}
