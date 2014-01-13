using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MigraineDiaryMVC.Models
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
			get { return String.Format("{0}, {1}", this.Name, this.Dosage.Display); }
		}
	}

	//public class IngredientsDBContext : DbContext
	//{
		
		
	//}
}