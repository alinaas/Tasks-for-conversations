using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace FileAssignment.Validators
{
	public class ValidDateStringAttribute : ValidationAttribute
	{
		protected override ValidationResult IsValid(object value, ValidationContext validationContext)
		{
			if (!DateTime.TryParseExact((string)value, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var _))
				return new ValidationResult("Invalid date format");

			return ValidationResult.Success;
		}
	}
}