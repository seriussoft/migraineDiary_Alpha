using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

//using MigraineDiaryMVC.Models;
using SeriusSoft.MigraineDiaryMVC.MigraineModels;

namespace MigraineDiaryMVC.ViewModels
{
	public class MigraineViewModel
	{
		public List<MigraineModel> Migraines { get; set; }

		public MigraineViewModel()
		{

			InstantiateMigraines(true);

		}

		private void InstantiateMigraines(bool shouldMock = false)
		{
			this.Migraines = new List<MigraineModel>();

			if (shouldMock)
			{
				this.Migraines.AddRange
				(
					new []
					{
						new MigraineModel
						{
							DateStarted = DateTime.Today,
							TimeStarted = DateTime.Today.AddHours(9),
							TimeEnded = DateTime.Today.AddHours(9).AddHours(3.5),
							Severity = 5
						}
					}
				);
			}
		}

		public void CreateMigraine(MigraineModel migraine)
		{
			this.Migraines.Add(migraine);
		}

		public void UpdateMigraine(MigraineModel migraine)
		{
			var toUpdate = Migraines.FirstOrDefault(m => m.ID == migraine.ID);
			toUpdate.Update(migraine);
		}
	}
}