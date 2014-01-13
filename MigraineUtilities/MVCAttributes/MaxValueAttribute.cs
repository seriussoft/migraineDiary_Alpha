using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeriusSoft.MigraineDiaryMVC.MigraineUtilities.MVCAttributes
{
    public class MaxValueAttribute : ValidationAttribute
    {
			private readonly double MaxValue;

			public MaxValueAttribute(double maxValue)
			{
				this.MaxValue = maxValue;
			}

			public override bool IsValid(object value)
			{
				return MaxValue.CompareTo(value) >= 0;
			}
    }
}
