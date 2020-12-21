using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace WebAtividadeEntrevista.Models.Validator
{
    public class CpfAttribute : ValidationAttribute, IClientValidatable
    {
        public CpfAttribute()
        {
            this.ErrorMessage = "The value {0} is invalid for CPF";
        }

        protected override ValidationResult IsValid(
            object value,
            ValidationContext validationContext)
        {
            if (value != null)
            {
                var valueValidLength = 11;
                var maskChars = new[] { ".", "-" };
                var multipliersForFirstDigit = new[] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
                var multipliersForSecondDigit = new[] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

                var mod11 = new Mod11();
                var isValid = mod11.IsValidCpf(value.ToString(), valueValidLength, maskChars, multipliersForFirstDigit, multipliersForSecondDigit);

                if (!isValid)
                {
                    return new ValidationResult(this.FormatErrorMessage(value.ToString()));
                }
            }

            return null;
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(
            ModelMetadata metadata,
            ControllerContext context)
        {
            var modelClientValidationRule = new ModelClientValidationRule
            {
                ValidationType = "cpf",
                ErrorMessage = this.FormatErrorMessage(metadata.DisplayName)
            };

            return new List<ModelClientValidationRule> { modelClientValidationRule };
        }
    }
}