using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MigraineDiaryMVC.Models
{
	public class SymptomModel
	{
		[Key]
		public int ID { get; set; }

		[Required]
		[Display(Name="Name")]
		public string Name { get; set; }
	}

	//public class SymptomsDBContext : DbContext
	//{
		
	//}
}