using CNX_Domain.Entities;
using FluentValidation;
using System.ComponentModel.DataAnnotations;

namespace CNX_Domain.Models
{
    public class UserFullVM : EntityBase<UserFullVM>
    {
        public string Id { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string UserName { get; set; }
        public string Locale { get; set; }
        [Required]
        public string UserPassword { get; set; }

        protected override void Validate()
        {
            ValidateUserName();
            ValidateUserEmail();
            ValidateUserPassword();
            ValidateUserHomeTown();
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

        private void ValidateUserPassword()
        {
            RuleFor(rule => rule.UserPassword)
                .NotEmpty().WithMessage("User password must be provided!");
        }
    }
}
