using FluentValidation;
using System.Collections.Generic;

namespace CNX_Domain.Entities
{
    public class User : EntityBase<User>
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string UserPassword { get; set; }
        public string UserHomeTown { get; set; }
        public virtual IList<PersonalNote> PersonalNoteList { get; set; }

        public User()
        {
            if (null == this.PersonalNoteList)
                this.PersonalNoteList = new List<PersonalNote>();
        }

        protected override void Validate()
        {
            ValidateUserName();
            ValidateUserPassword();
            ValidateUserHomeTown();
            ValidateUserEmail();
        }

        private void ValidateUserName()
        {
            RuleFor(rule => rule.UserName)
                .NotEmpty().WithMessage("User name must be provided!");
        }

        private void ValidateUserPassword()
        {
            RuleFor(rule => rule.UserPassword)
                .NotEmpty().WithMessage("User password must be provided!");
        }

        private void ValidateUserHomeTown()
        {
            RuleFor(rule => rule.UserHomeTown)
                .NotEmpty().WithMessage("User hometown must be provided!");
        }

        private void ValidateUserEmail()
        {
            RuleFor(rule => rule.UserEmail)
                .NotEmpty().WithMessage("User email must be provided!")
                .EmailAddress().WithMessage("User email must be valid!");
        }
    }
}
