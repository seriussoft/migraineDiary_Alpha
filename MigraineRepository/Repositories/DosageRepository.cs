using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeriusSoft.MigraineDiaryMVC.MigraineRepository.Repositories
{
	public class DosageRepository : BaseDisposableRepository, Interfaces.IDosageRepository
	{
		public DosageRepository(MigraineDiaryMVC_DBContext context, bool saveOnChange = true) : base(context, saveOnChange) { }

		#region IDosageRepository Members

		#region Get
		public IEnumerable<MigraineModels.DosageModel> GetDoses()
		{
			return this.Context.Dosage.Include(d => d.UnitOfMeasurement);
		}

		public IEnumerable<MigraineModels.DosageModel> GetDosesMinimal()
		{
			return this.Context.Dosage.ToList();
		}

		public MigraineModels.DosageModel GetDoseByID(int id)
		{
			return this.Context.Dosage.Find(id);
		}

		public MigraineModels.DosageModel GetDoseMinimalByID(int id)
		{
			return this.Context.Dosage.Find(id);
		}

		#endregion

		#region Create
		public MigraineModels.DosageModel CreateDose(MigraineModels.DosageModel dose)
		{
			var returnDose = this.Context.Dosage.Add(dose);
			this.Save(this.SaveOnChange);

			return returnDose;
		}

		public bool CreateDoseIfValid(MigraineModels.DosageModel dose, bool isValid)
		{
			if (isValid)
			{
				this.CreateDose(dose);
			}

			return isValid;
		}

		#endregion

		#region Update
		public void UpdateDose(MigraineModels.DosageModel dose)
		{
			this.Context.Entry(dose).State = EntityState.Modified;
			this.Save(this.SaveOnChange);
		}

		public bool UpdateDoseIfValid(MigraineModels.DosageModel dose, bool isValid)
		{
			if (isValid)
			{
				this.UpdateDose(dose);
			}

			return isValid;
		}

		#endregion

		#region Delete
		public void DeleteDose(int id)
		{
			var dose = this.Context.Dosage.Find(id);
			this.DeleteDose(dose);
		}

		public void DeleteDose(MigraineModels.DosageModel dose)
		{
			this.Context.Dosage.Remove(dose);
			this.Save(this.SaveOnChange);
		}

		#endregion

		#endregion
	}
}
