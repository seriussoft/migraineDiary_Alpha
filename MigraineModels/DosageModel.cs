using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SeriusSoft.MigraineDiaryMVC.MigraineUtilities.MVCAttributes;

namespace SeriusSoft.MigraineDiaryMVC.MigraineModels
{
	public class DosageModel
	{
		[Key]
		public int DosageID { get; set; }

		[Required]
		[MinValue(0)]
		[Display(Name="Quantity")]
		public double Quantity { get; set; }

		[Required]
		[Display(Name="Units")]
		public int UnitOfMeasurementID { get; set; }
		
		[ForeignKey("UnitOfMeasurementID")]
		public virtual UnitOfMeasurementModel UnitOfMeasurement { get; set; }

		public string Display
		{
			get
			{
				if (this.UnitOfMeasurement == null)
					return String.Empty;

				return String.Format("{0} {1}", this.Quantity, this.UnitOfMeasurement.Name); 
			}
		}
	}

	//public class DosagesDBContext : DbContext
	//{
		
	//}
}