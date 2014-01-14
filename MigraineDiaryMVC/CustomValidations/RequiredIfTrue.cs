using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MigraineDiaryMVC.CustomValidations
{
	public class RequiredIf : ValidationAttribute
	{
		protected string FieldNameToCompare { get; set; }
		protected object ExpectedValue { get; set; }

		public RequiredIf(string fieldNameToCompare, object expectedValue, string errorMessage = "{0} is required.") : base(errorMessage)
		{
			this.FieldNameToCompare = fieldNameToCompare;
			this.ExpectedValue = expectedValue;
		}

		protected override ValidationResult IsValid(object value, ValidationContext validationContext)
		{
			var instance = validationContext.ObjectInstance;
			var propertyInstance = instance.GetType().GetProperty(this.FieldNameToCompare);

			if (propertyInstance == null)
				return new ValidationResult(String.Format("cannot find property '{0}'", this.FieldNameToCompare));

			var propertyValue = propertyInstance.GetValue(instance, null);

			//using .Equals because this is an object wrapped around the bool. we need to do a true value comparison, not a ref comparison
			if (!propertyValue.Equals(this.ExpectedValue))
				return ValidationResult.Success;

			if (value == null)
				return new ValidationResult(this.ErrorMessage);

			return ValidationResult.Success;
		}
	}
}