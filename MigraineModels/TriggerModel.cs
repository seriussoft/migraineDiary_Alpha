using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;

using SeriusSoft.MigraineDiaryMVC.MigraineUtilities.MVCAttributes;

namespace SeriusSoft.MigraineDiaryMVC.MigraineModels
{
	public class TriggerModel
	{
		[Key]
		public int TriggerID { get; set; }

		[Display(Name = "Type")]
		public int TriggerTypeID { get; set; }

		[ForeignKey("TriggerTypeID")]
		public virtual TriggerTypeModel TriggerType { get; set; }

		[Required]
		[Display(Name = "Name")]
		[MinLength(3)]
		public string Name { get; set; }

		[Display(Name="Info")]
		public string Info {get;set;}

	}

	public class TriggerTypeModel
	{
		[Key]
		public int TriggerTypeID { get; set; }

		[Required]
		[Display(Name = "Name")]
		[MinLength(3)]
		public string Name { get; set; }
	}
}
