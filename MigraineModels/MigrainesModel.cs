using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SeriusSoft.MigraineDiaryMVC.MigraineModels.Interfaces;

namespace SeriusSoft.MigraineDiaryMVC.MigraineModels
{
	public class MigrainesModel : ICloneable, IUpdateable
	{
		public bool HasErrors { get { return this.Errors != null && this.Errors.Any(); } }
		public bool HasWarnings { get { return this.Warnings != null && this.Warnings.Any(); } }

		public List<string> Errors { get; private set; }
		public List<string> Warnings { get; private set; }

		public IEnumerable<MigraineModel> AllMigraineModels { get; set; }
		public IEnumerable<MigraineModel> MigrainesInScope
		{
			get
			{
				var migrainesBefore = 
					this.EndDateRange.HasValue
						? this.AllMigraineModels.Where(m => m.DateStarted <= this.EndDateRange.Value)
						: this.AllMigraineModels;

				var migrainesInScope =
					this.StartDateRange.HasValue
						? migrainesBefore.Where(m => m.DateStarted >= this.StartDateRange.Value)
						: migrainesBefore;

				var migrainesToDisplay = migrainesInScope.ToList();

				return migrainesToDisplay;
				//var migraines = from migraine in this.AllMigraineModels
				//								where 
				//									(!this.StartDateRange.HasValue || migraine.DateStarted >= this.StartDateRange)
				//									&& (!this.EndDateRange.HasValue || migraine.DateStarted <= this.EndDateRange)
				//								orderby migraine.DateStarted, migraine.TimeStarted
				//								select migraine;

				//return migraines.ToList();
			}
		}

		public DateTime? StartDateRange { get; set; }
		public DateTime? EndDateRange { get; set; }

		public MigraineModel DefaultMigraine { get; private set; }

		public MigrainesModel(IEnumerable<MigraineModel> migraines = null)
		{
			this.AllMigraineModels = migraines ?? new List<MigraineModel>();
			this.Errors = new List<string>();
			this.Warnings = new List<string>();
			this.DefaultMigraine = new MigraineModel();
		}

		#region ICloneable Members

		public object Clone()
		{
			var clone = new MigrainesModel().Update(this);

			return clone;
		}

		#endregion	ICloneable Members

		#region IUpdateable Members

		public MigrainesModel Update(MigrainesModel migraines, bool replaceOldData = true)
		{
			this.StartDateRange = migraines.StartDateRange;
			this.EndDateRange = migraines.EndDateRange;

			if (replaceOldData)
			{
				this.Errors.Clear();
				this.Warnings.Clear();
			}

			this.Errors.AddRange(migraines.Errors);
			this.Warnings.AddRange(migraines.Warnings);

			return this;
		}

		public object Update(object migraine)
		{
			return this.Update(migraine as MigraineModel);
		}

		#endregion	IUpdateable Members
	}
}
