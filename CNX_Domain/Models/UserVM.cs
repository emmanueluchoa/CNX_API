﻿using CNX_Domain.Entities;
using FluentValidation;

namespace CNX_Domain.Models
{
    public class UserVM : EntityBase<UserVM>
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Locale { get; set; }

        protected override void Validate()
        {
            ValidateUserName();
            ValidateUserHomeTown();
            ValidateUserEmail();
        }

        private void ValidateUserName()
        {
            RuleFor(rule => rule.UserName)
                .NotEmpty().WithMessage("User name must be provided!");
        }

        private void ValidateUserHomeTown()
        {
            RuleFor(rule => rule.Locale)
                .NotEmpty().WithMessage("User hometown must be provided!");
        }

        private void ValidateUserEmail()
        {
            RuleFor(rule => rule.Email)
                .NotEmpty().WithMessage("User email must be provided!")
                .EmailAddress().WithMessage("User email must be valid!");
        }
    }
}
