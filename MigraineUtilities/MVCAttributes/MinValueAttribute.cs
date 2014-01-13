using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeriusSoft.MigraineDiaryMVC.MigraineUtilities.MVCAttributes
{
    public class MinValueAttribute : ValidationAttribute
    {
			private readonly double MinValue;

			public MinValueAttribute(double maxValue)
			{
				this.MinValue = maxValue;
			}

			public override bool IsValid(object value)
			{
				return MinValue.CompareTo(value) <= 0;
			}
    }
}
