using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SeriusSoft.MigraineDiaryMVC.MigraineUtilities.MVCAttributes;

namespace SeriusSoft.MigraineDiaryMVC.MigraineModels
{
	public class MedicationModel
	{
		[Key]
		public int MedicationID { get; set; }

		[Display(Name = "Dosage")]
		public int DosageID { get; set; }
		
		[ForeignKey("DosageID")]
		public virtual DosageModel Dosage { get; set; }

		[Required]
		[Display(Name="Name")]
		[MinLength(3)]
		public string Name { get; set; }


		[Display(Name="Ingredients")]
		public List<int> IngredientIDs { get; set; }

		[InverseProperty("Medications")]
		//[Display(Name="Ingredients")]
		public ICollection<IngredientModel> Ingredients { get; set; }
	}

	//public class MedicationsDBContext : DbContext
	//{
		
	//}
	
}