using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeriusSoft.MigraineDiaryMVC.MigraineModels.Interfaces
{
	public interface IUpdateable : IUpdateable<object>
	{

	}

	public interface IUpdateable<T>
	{
		T Update(T newValuesContainer);
	}
}
