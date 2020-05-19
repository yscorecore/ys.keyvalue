using System.ComponentModel.DataAnnotations;

namespace YS.KeyValue
{
    public class CategoryRuleAttribute : MixedValidationAttribute//RegularExpressionAttribute
    {
        public CategoryRuleAttribute()
            : base(
                new RequiredAttribute(),
                new RegularExpressionAttribute("^[a-zA-Z_][a-zA-Z0-9_]*(\\.[a-zA-Z_][a-zA-Z0-9_]*)*$")
            )
        {
        }
    }

    public class MixedValidationAttribute : ValidationAttribute
    {
        private readonly ValidationAttribute[] validations;

        public MixedValidationAttribute(params ValidationAttribute[] validations)
        {
            this.validations = validations;
        }

        public override bool IsValid(object value)
        {
            foreach (var validation in validations)
            {
                if (!validation.IsValid(value))
                {
                    return false;
                }
            }
            return true;
        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            foreach (var validation in validations)
            {
                var result = validation.GetValidationResult(value, validationContext);
                if (result != ValidationResult.Success)
                {
                    return result;
                }
            }
            return ValidationResult.Success;
        }

    }
}
