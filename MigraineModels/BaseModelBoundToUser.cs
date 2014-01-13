using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeriusSoft.MigraineDiaryMVC.MigraineModels
{
	public class BaseModelBoundToUser : IModelBoundToUser
	{
		//[ForeignKey]
		public string UserID { get; set; }
	}

	public interface IModelBoundToUser
	{
		string UserID { get; set; }
	}
}
