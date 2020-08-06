using AutoMapper;
using CNX_Domain.Entities;
using CNX_Domain.Interfaces.Application;
using CNX_Domain.Interfaces.Repository;
using CNX_Domain.Models;
using System;
using System.Collections.Generic;

namespace CNX_Domain.Application
{
    public class UserApplication : IUserApplication
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserApplication(IUserRepository userRepository, IMapper mapper)
        {
            this._userRepository = userRepository;
            this._mapper = mapper;
        }

        public IList<UserVM> GetUsers() =>
            this._mapper.Map<IList<UserVM>>(this._userRepository.GetAll());

        public UserVM GetById(string id) =>
            this._mapper.Map<UserVM>(this._userRepository.GetById(id));

        public UserVM CreateUser(UserVM userVM)
        {
            var user = this._mapper.Map<User>(userVM);

            if (user.IsValid())
            {
                if (ValidateIfUserExists(user.UserName, user.UserPassword))
                    throw new Exception("User already exists.");

                user = this._userRepository.Insert(user);
                userVM = this._mapper.Map<UserVM>(user);
                return userVM;
            }
            else
                throw new Exception(user.GetValidationErrors());
        }

        public bool ValidateIfUserExists(string userName, string userPassword) =>
           this._userRepository.HasAny(param => param.UserName.Equals(userName) && param.UserPassword.Equals(userPassword));

        public UserVM UpdateUser(UserVM userVM)
        {
            var user = this._mapper.Map<User>(userVM);

            if (user.IsValid())
            {
                var teste = this._userRepository.Update(user);
                return this._mapper.Map<UserVM>(user);
            }
            else
                throw new Exception(user.GetValidationErrors());
        }

        public UserVM GetUser(string userName, string userPassword)
        {
            CheckUserName(userName);
            CheckUserPassword(userPassword);

            User user = this._userRepository.GetFirst(user => user.UserName.Equals(userName) && user.UserPassword.Equals(userPassword));
            if (null != user)
                return this._mapper.Map<UserVM>(user);
            else
                throw new Exception("User not found.");
        }

        private static void CheckUserPassword(string userPassword)
        {
            if (string.IsNullOrWhiteSpace(userPassword))
                throw new Exception("User password not provided!");
        }

        private static void CheckUserName(string userName)
        {
            if (string.IsNullOrWhiteSpace(userName))
                throw new Exception("User name not provided.");
        }
    }
}
