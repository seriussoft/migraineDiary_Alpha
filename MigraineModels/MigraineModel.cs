using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using SeriusSoft.MigraineDiaryMVC.MigraineModels.Interfaces;

namespace SeriusSoft.MigraineDiaryMVC.MigraineModels
{
	public class MigraineModel : ICloneable, IUpdateable, IUpdateable<MigraineModel>
	{
		[Key]
		public Guid ID { get; protected set; }

		[Required]
		[DisplayName("Date Migraine Started")]
		[DisplayFormat(DataFormatString="{0:yyy-MM-dd}", ApplyFormatInEditMode=true)]
		[DataType(DataType.Date)]
		public DateTime DateStarted { get; set; }

		[Required]
		[DisplayName("Time Migraine Started")]
		[DisplayFormat(DataFormatString="{0:HH:mm}", ApplyFormatInEditMode=true)]
		[DataType(DataType.Time)]
		public DateTime TimeStarted { get; set; }

		[Required]
		[DisplayName("Time Migraine Ended")]
		[DataType(DataType.Time)]
		public DateTime TimeEnded { get; set; }

		[Required]
		[DisplayName("Severity (1-10)")]
		[Range(1,10)]
		public int Severity { get; set; }

		[DisplayName("Comments")]
		[DataType(DataType.MultilineText)]
		public string Comment { get; set; }

		public MigraineModel()
		{
			this.ID = Guid.NewGuid();
		}

		protected MigraineModel(Guid id)
		{
			this.ID = id;
		}
	
		#region ICloneable Members

		public object Clone()
		{
			var clone = new MigraineModel(this.ID).Update(this);

			return clone;
		}

		#endregion	ICloneable Members

		#region IUpdateable Members

		public MigraineModel Update(MigraineModel migraine)
		{
			this.DateStarted = migraine.DateStarted;
			this.TimeStarted = migraine.TimeStarted;
			this.TimeEnded = migraine.TimeEnded;
			this.Severity = migraine.Severity;

			return this;
		}

		public object Update(object migraine)
		{
			return this.Update(migraine as MigraineModel);
		}

		#endregion	IUpdateable Members

	}
}