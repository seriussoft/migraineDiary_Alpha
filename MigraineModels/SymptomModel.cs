using System;
using System.ComponentModel.DataAnnotations;

namespace SeriusSoft.MigraineDiaryMVC.MigraineModels
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