using FluentValidation;
using FluentValidation.Results;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace CNX_Domain.Entities
{
    public abstract class EntityBase<T> : AbstractValidator<T> where T : EntityBase<T>
    {
        [NotMapped]
        public ValidationResult ValidationResult { get; set; }
        protected EntityBase()
        {
            ValidationResult = new ValidationResult();
        }

        protected abstract void Validate();

        public void AddCustomValidationFailure(string property, string errorMessage) =>
            this.ValidationResult.Errors.Add(new ValidationFailure(property, errorMessage));

        public void AddErrors(IList<ValidationFailure> errors)
        {
            foreach (var error in errors)
                AddCustomValidationFailure(error.PropertyName, error.ErrorMessage);
        }

        public bool IsValid()
        {
            Validate();
            return this.ValidationResult.IsValid;
        }

        public string GetValidationErrors() =>
            string.Join(", ", this.ValidationResult.Errors.Select(error => $"{error.ErrorCode}: {error.ErrorMessage}"));
    }
}
