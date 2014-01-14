using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using SeriusSoft.MigraineDiaryMVC.MigraineModels;
using MigraineDiaryMVC.CustomValidations;

namespace MigraineDiaryMVC.ViewModels
{
	public class EditableMigraineViewModel
	{
		public EditableMigraineViewModel(Guid? id)
		{
			this.ID = id ?? Guid.NewGuid();
			if (this.ID == Guid.Empty)
				this.ID = Guid.NewGuid();
		}

		public EditableMigraineViewModel() : this(null) { }

		[Key]
		public Guid ID { get; set; }

		[DisplayName("Is the migraine still happening")]
		public bool StillHappening { get; set; }

		[Required]
		[DisplayName("Date Migraine Started")]
		[DisplayFormat(DataFormatString = "{0:yyy-MM-dd}", ApplyFormatInEditMode = true)]
		[DataType(DataType.Date)]
		public DateTime DateStarted { get; set; }

		[Required]
		[DisplayName("Time Migraine Started")]
		//[DisplayFormat(DataFormatString = "{0:s}", ApplyFormatInEditMode = true)]
		[DisplayFormat(DataFormatString = "{0:hh:mm}", ApplyFormatInEditMode = true)]
		[DataType(DataType.Time)]
		public DateTime TimeStarted { get; set; }

		//[Required]
		[RequiredIf("StillHappening", false, "'{0}' is required if the migraine has finished.")]
		[DisplayName("Time Migraine Ended")]
		//[DisplayFormat(DataFormatString = "{0:s}", ApplyFormatInEditMode = true)]
		[DisplayFormat(DataFormatString = "{0:hh:mm}", ApplyFormatInEditMode = true)]
		[DataType(DataType.Time)]
		public DateTime? TimeEnded { get; set; }

		[Required]
		[DisplayName("Severity (1-10)")]
		[Range(1, 10)]
		public int Severity { get; set; }

		[DisplayName("Comments")]
		[DataType(DataType.MultilineText)]
		public string Comment { get; set; }

		public MigraineModel ToMigraineModel()
		{
			var model = new MigraineModel()
			{
				DateStarted = this.DateStarted,
				Severity = this.Severity,
				TimeStarted = this.TimeStarted,//DateTime.Parse(this.TimeStarted),
				TimeEnded = this.TimeEnded, //DateTime.Parse(this.TimeEnded),
				StillHappening = this.StillHappening,
				Comment = this.Comment
			};

			return model;
		}

		public static EditableMigraineViewModel FromMigraineModel(MigraineModel model)
		{
			var viewModel = new EditableMigraineViewModel(model.ID)
			{
				DateStarted = model.DateStarted,
				TimeStarted = model.TimeStarted, //model.TimeStarted.ToString("s"),
				TimeEnded = model.TimeEnded, //model.TimeEnded.ToString("s"),
				Severity = model.Severity,
				Comment = model.Comment,
				StillHappening = model.StillHappening,
				ID = model.ID
			};

			return viewModel;
		}
	}
}