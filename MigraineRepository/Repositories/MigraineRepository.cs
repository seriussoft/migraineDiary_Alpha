using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SeriusSoft.Tools.Extensions;

namespace SeriusSoft.MigraineDiaryMVC.MigraineRepository.Repositories
{
	public class MigraineRepository : BaseDisposableRepository, Interfaces.IMigraineRepository
	{
		public MigraineRepository(MigraineDiaryMVC_DBContext context, bool saveOnChange = true) : base(context, saveOnChange) { }

		#region IMigraineRepository Members

		public IEnumerable<MigraineModels.MigraineModel> GetMigraines()
		{
			throw new NotImplementedException();
		}

		public IEnumerable<MigraineModels.MigraineModel> GetMigrainesMinimal()
		{
			throw new NotImplementedException();
		}

		public MigraineModels.MigraineModel GetMigraineByID(Guid id)
		{
			throw new NotImplementedException();
		}

		public MigraineModels.MigraineModel GetMigraineMinimalByID(Guid id)
		{
			throw new NotImplementedException();
		}

		public MigraineModels.MigraineModel CreateMigraine(MigraineModels.MigraineModel migraine)
		{
			throw new NotImplementedException();
		}

		public bool CreateMigraineIfValid(MigraineModels.MigraineModel migraine, bool isValid)
		{
			throw new NotImplementedException();
		}

		public void UpdateMigraine(MigraineModels.MigraineModel migraine)
		{
			throw new NotImplementedException();
		}

		public bool UpdateMigraineIfValid(MigraineModels.MigraineModel migraine, bool isValid)
		{
			throw new NotImplementedException();
		}

		public void DeleteMigraine(Guid id)
		{
			throw new NotImplementedException();
		}

		public void DeleteMigrain(MigraineModels.MigraineModel migraine)
		{
			throw new NotImplementedException();
		}

		#endregion

		#region IMigraineRepository Members (DateBased)


		public IEnumerable<MigraineModels.MigraineModel> GetMigrainesInDateRange(DateTime startDate, DateTime endDate)
		{
			return this.Context.Migraines.WhereBetween(m => m.DateStarted, startDate, endDate).ToList();
		}

		/// <summary>
		/// If no anyDayInMonth is supplied, then DateTime.Today is used as a basis for discovering the month
		/// </summary>
		/// <param name="anyDayInMonth"></param>
		/// <returns></returns>
		public IEnumerable<MigraineModels.MigraineModel> GetMigrainesInMonth(DateTime? anyDayInMonth = null)
		{
			var workingDay = anyDayInMonth ?? DateTime.Today;

			var firstDayOfMonth = workingDay.FirstDayOfMonth();
			var lastDayOfMonth = workingDay.LastDayOfMonth();

			return GetMigrainesInDateRange(DateTime.Today, DateTime.Today);
		}

		#endregion	IMigraineRepository Members (DateBased)
	}
}
