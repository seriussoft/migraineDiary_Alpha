using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MigraineDiaryMVC.Models;
using SeriusSoft.MigraineDiaryMVC.MigraineRepository;
using SeriusSoft.MigraineDiaryMVC.MigraineRepository.Interfaces;
using SeriusSoft.MigraineDiaryMVC.MigraineRepository.Repositories;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;

namespace MigraineDiaryMVC.Controllers
{
	public class BaseMigraineController : Controller
	{
		public MigraineDiaryMVC_DBContext DataContext { get; set; }

		public IDosageRepository DosageRepository { get; set; }
		public UserManager<ApplicationUser> UserManager { get; set; }


		public BaseMigraineController()
		{
			this.DataContext = new MigraineDiaryMVC_DBContext();
			this.UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
		}

		//protected override void Dispose(bool disposing)
		//{
		//	DataContext.Dispose();
		//	base.Dispose(disposing);
		//}

		public async Task<ApplicationUser> GetCurrentUserAsync()
		{
			if (!HttpContext.User.Identity.IsAuthenticated || HttpContext.User.Identity == null)// || Guid.TryParse(HttpContext.User.Identity.GetUserId(), out userID))
				return null;

			var userID = HttpContext.User.Identity.GetUserId();
			var user = await this.UserManager.FindByIdAsync(userID);

			return user;
		}

		public ApplicationUser GetCurrentUser()
		{
			if (!HttpContext.User.Identity.IsAuthenticated || HttpContext.User.Identity == null)// || Guid.TryParse(HttpContext.User.Identity.GetUserId(), out userID))
				return null;

			var userID = HttpContext.User.Identity.GetUserId();
			var user = this.UserManager.FindById(userID);

			return user;
		}

		public string GetCurrentUserID()
		{
			return HttpContext.User.Identity.GetUserId();
		}

		#region SetupMethods

		protected void SetupDosageRepository()
		{
			this.DosageRepository = new DosageRepository(this.DataContext);
		}

		#endregion
	}

	public class BaseDisposableMigraineController : BaseMigraineController
	{
		protected override void Dispose(bool disposing)
		{
			this.DataContext.Dispose();
			base.Dispose(disposing);
		}
	}
}