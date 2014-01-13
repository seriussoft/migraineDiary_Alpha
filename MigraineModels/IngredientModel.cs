using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SeriusSoft.MigraineDiaryMVC.MigraineModels
{
	public class IngredientModel
	{
		[Key]
		public int IngredientID { get; set; }

		[Required]
		[Display(Name="Name")]
		public string Name { get; set; }

		[Display(Name = "Dosage")]
		public int DosageID { get; set; }

		[ForeignKey("DosageID")]
		public virtual DosageModel Dosage { get; set; }

		[InverseProperty("Ingredients")]
		public List<MedicationModel> Medications { get; set; }

		public string Display
		{
			get { return String.Format("{0}, {1}", this.Name, (this.Dosage ?? new DosageModel()).Display); }
		}
	}

	//public class IngredientsDBContext : DbContext
	//{
		
		
	//}
}