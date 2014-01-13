using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;
using MigraineUtilities.MVCAttributes;

namespace MigraineDiaryMVC.Models
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
			get { return String.Format("{0} {1}", this.Quantity, this.UnitOfMeasurement.Name); }
		}
	}

	//public class DosagesDBContext : DbContext
	//{
		
	//}
}