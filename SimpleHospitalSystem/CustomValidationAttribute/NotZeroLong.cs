using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleHospitalSystem.CustomValidationAttribut
{

    public class NotZeroLongAttribute : ValidationAttribute
    {
        public string GetErrorMessage() =>
            $"This field can't be zero";

        protected override ValidationResult IsValid(object value,
            ValidationContext validationContext)
        {
            if (value == null)
            {
                return new ValidationResult(GetErrorMessage());
            }

            if ((long)value == 0)
            {
                return new ValidationResult(GetErrorMessage());
            }

            return ValidationResult.Success;
        }
    }
}
