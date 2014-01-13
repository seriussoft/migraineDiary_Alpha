using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SeriusSoft.MigraineDiaryMVC.MigraineModels;

namespace SeriusSoft.MigraineDiaryMVC.MigraineRepository
{
	public static class MigraineDiaryMVC_DBContext_Extensions
	{
		public static IQueryable<TModel> WhereBoundToUser<TModel>(this IQueryable<TModel> collection, string userID) where TModel : BaseModelBoundToUser
		{
			return collection.Where(i => i.UserID == userID);
		}
	}
}
