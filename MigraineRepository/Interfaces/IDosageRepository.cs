using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SeriusSoft.MigraineDiaryMVC.MigraineModels;

namespace SeriusSoft.MigraineDiaryMVC.MigraineRepository.Interfaces
{
	public interface IDosageRepository
	{
		IEnumerable<DosageModel> GetDoses();
		IEnumerable<DosageModel> GetDosesMinimal();

		DosageModel GetDoseByID(int id);
		DosageModel GetDoseMinimalByID(int id);

		DosageModel CreateDose(DosageModel dose);
		bool CreateDoseIfValid(DosageModel dose, bool isValid);

		void UpdateDose(DosageModel dose);
		bool UpdateDoseIfValid(DosageModel dose, bool isValid);
		
		void DeleteDose(int id);
		void DeleteDose(DosageModel dose);

		void Save();
	}
}
