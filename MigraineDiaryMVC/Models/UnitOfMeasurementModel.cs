using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MigraineDiaryMVC.Models
{
	public class UnitOfMeasurementModel
	{
		[Key]
		public int UnitOfMeasurementID { get; set; }

		[Required]
		[Display(Name="Unit Of Measurement")]
		[DataType(DataType.Text)]
		public string Name { get; set; }
	}

	//public class UnitOfMeasurementDBContext : DbContext
	//{
	//	public DbSet<UnitOfMeasurementModel> UnitsOfMeasurement { get; set; }
	//}
}