using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SeriusSoft.MigraineDiaryMVC.MigraineModels
{
	public class UnitOfMeasurementModel : BaseModelBoundToUser
	{
		[Key]
		public int UnitOfMeasurementID { get; set; }

		[Required]
		[Display(Name="Unit Of Measurement")]
		[DataType(DataType.Text)]
		public string Name { get; set; }
	}
}