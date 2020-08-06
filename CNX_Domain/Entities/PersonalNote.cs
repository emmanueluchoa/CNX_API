using FluentValidation;

namespace CNX_Domain.Entities
{
    public class PersonalNote : EntityBase<PersonalNote>
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string Note { get; set; }
        public virtual User User { get; set; }

        protected override void Validate()
        {
            ValidateUserId();
            ValidateNote();
        }

        private void ValidateUserId()
        {
            RuleFor(rule => rule.UserId)
                .NotEmpty().WithMessage("User id must be provided.");
        }

        private void ValidateNote()
        {
            RuleFor(rule => rule.Note)
                .NotEmpty().WithMessage("Note must be provided.")
                .MaximumLength(140).WithMessage("Note must be a maximum of 140 characters.");
        }
    }
}
