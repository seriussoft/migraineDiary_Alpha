using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SeriusSoft.MigraineDiaryMVC.MigraineModels;

namespace SeriusSoft.MigraineDiaryMVC.MigraineRepository.Interfaces
{
	public interface IMigraineRepository
	{
		IEnumerable<MigraineModel> GetMigraines();
		IEnumerable<MigraineModel> GetMigrainesMinimal();

		IEnumerable<MigraineModel> GetMigrainesInDateRange(DateTime startDate, DateTime endDate);
		IEnumerable<MigraineModel> GetMigrainesInMonth(DateTime? anyDayInMonth);

		MigraineModel GetMigraineByID(Guid id);
		MigraineModel GetMigraineMinimalByID(Guid id);

		MigraineModel CreateMigraine(MigraineModel migraine);
		bool CreateMigraineIfValid(MigraineModel migraine, bool isValid);

		void UpdateMigraine(MigraineModel migraine);
		bool UpdateMigraineIfValid(MigraineModel migraine, bool isValid);

		void DeleteMigraine(Guid id);
		void DeleteMigrain(MigraineModel migraine);

		void Save();
	}
}
