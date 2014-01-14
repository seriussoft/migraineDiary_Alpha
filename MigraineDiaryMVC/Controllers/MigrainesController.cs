using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MigraineDiaryMVC.Models;
using MigraineDiaryMVC.ViewModels;
using SeriusSoft.MigraineDiaryMVC.MigraineModels;
using SeriusSoft.MigraineDiaryMVC.MigraineRepository;

namespace MigraineDiaryMVC.Controllers
{
	//[Route("migraine-history")]
	[Route("{action=migraine-management}")]
	[Authorize(Roles = RoleConstants.Customer)]
	public class MigrainesController : BaseDisposableMigraineController
	{
		//[Route("migraine-management")]
		//public ActionResult Index()
		//{
		//	var migraines = this.DataContext.Migraines.OrderBy(m => m.DateStarted).ThenBy(m => m.TimeStarted).ToList();
		//	var migrainesWrapper = new MigrainesModel()
		//	{
		//		AllMigraineModels = migraines
		//	};

		//	return View(migrainesWrapper);
		//}

		//[Route("~/migraine-management", Name="index")]
		//public ActionResult Index()
		//{
		//	return Index(startDate: null, endDate: null);
		//}

		//[Route("~/migraine-management")]
		[Route("~/migraines-between-{startDate:datetime?}-and-{endDate:datetime?}", Name="MigrainesBetween")]
		[Route("~/migraine-management", Name="MigraineManagement")]
		[Route("~/Migraines", Name="MigraineIndex")]
		public ActionResult Index(DateTime? startDate = null, DateTime? endDate = null, IEnumerable<string> errors = null, IEnumerable<string> warnings = null)
		{
			var migrainesWrapper = HelperMethods.GetBasicMigrainesModel(this.DataContext);

			if(errors != null)
				migrainesWrapper.Errors.AddRange(errors);

			if (warnings != null)
				migrainesWrapper.Warnings.AddRange(warnings);

			migrainesWrapper.StartDateRange = startDate;
			migrainesWrapper.EndDateRange = endDate;

			return View(migrainesWrapper);
		}

		#region Index by dates

		[Route("~/migraines-before-{endDate:datetime?}")]
		public ActionResult MigrainesBefore(DateTime? endDate = null)
		{
			if(endDate.HasValue)
				return Index(endDate: endDate);
			
			return Index(errors: new[]{"You forgot to pick an end date. Here's the entire migraine history instead."});
		}

		[Route("~/migraines-after-{startDate:datetime?}")]
		public ActionResult MigrainesAfter(DateTime? startDate)
		{
			if (startDate.HasValue)
				return Index(startDate: startDate);

			return Index(errors: new[] { "You forgot to pick a start date. Here's the entire migraine history instead." });

			//DateTime normalizedEndDate;
			//var errorIndexView = ReturnErrorIndexViewIfMissingDate(startDate, out normalizedEndDate, "You forgot to pick a start date. Here's the entire migraine history instead.");
			//if (errorIndexView != null)
			//	return errorIndexView;

			//return Index(startDate:null, endDate: normalizedEndDate);
		}

		[HttpGet]
		public ActionResult MigrainesBetween(string startDate = "", string endDate = "")
		{
			DateTime normalizedStartDate, normalizedEndDate;
			var errorIndexView = ReturnErrorIndexViewIfMissingDate(startDate, out normalizedStartDate, "You forgot to pick a start date. Here's the entire migraine history instead.");
			if (errorIndexView != null)
				return errorIndexView;

			errorIndexView = ReturnErrorIndexViewIfMissingDate(endDate, out normalizedEndDate, "You forgot to pick an end date. Here's the entire migraine history instead.");
			if (errorIndexView != null)
				return errorIndexView;

			return Index(startDate: normalizedStartDate, endDate: normalizedEndDate);
		} 

		#endregion	Index by dates

		

		public ActionResult MonthlyCalendar()
		{
			return View("MonthlyCalendar", null as MigraineModel);
			//
		}

		#region Details

		[Route("most-recent-migraine")]
		public ActionResult GetLatestMigraine()
		{
			var migraineModel = this.DataContext.Migraines.OrderByDescending(m => m.DateStarted).FirstOrDefault();
			if (migraineModel == null)
			{
				return Index(); // need to display "no migraines could be found"
			}

			return View("Details", migraineModel);
		}

		[Route("migraine-on-{date:datetime?}")]
		[Route("migraine-on-")]
		public ActionResult DetailsByDate(DateTime? date = null)
		{
			//http://localhost:35746/migraine-on-2013-02-05

			if (!date.HasValue)
				return View("Index", HelperMethods.GetMigrainesModelWithErrors(this.DataContext, "You forgot to include a date to search by. We returned all the migraines instead." ));

			var migraineModel = DataContext.Migraines.FirstOrDefault(m => m.DateStarted == date);
			if (migraineModel == null)
			{
				var message = String.Format("We could not find any migraines on '{0:yyyy-dd-MM}' so we returned the whole migraine history instead.", date);
				return View("Index", HelperMethods.GetMigrainesModelWithWarnings(this.DataContext, message)); //need to display "nothing could be found" message above the index page
			}

			return View("DetailsByDate", migraineModel);
		}

		//[Route("recent-migraine-on-{date?}")]
		//[Route("Migraines/DetailsByDate/{date?}")]
		//public ActionResult DetailsByDate(string date = "")
		//{
		//	var searchDate = GetDateFromString(date);

		//	var migraineModel = DataContext.Migraines.ToList().FirstOrDefault(m => m.DateStarted == searchDate);
		//	if (migraineModel == null)
		//	{
		//		return HttpNotFound();
		//	}
		//	return View("DetailsByDate", migraineModel);
		//}

		[Route("historical-migraine-{id?}")]
		public ActionResult Details(string id = "")
		{
			var migraineModel = HelperMethods.FindFromGuid(DataContext, id);

			if (migraineModel == null)
			{
				//return HttpNotFound();
				return Index(); //need to display "nothing could be found" message above the index page
			}

			return View(migraineModel);
		} 

		#endregion	Details
		
		#region Create

		[Route("log-new-migraine")]
		public ActionResult Create()
		{
			return View();
		}

		[HttpPost]
		[Route("log-new-migraine")]
		//public ActionResult Create(MigraineModel migraineModel)
		public ActionResult Create(EditableMigraineViewModel migraineViewModel)
		{
			if (ModelState.IsValid)
			{
				var migraineModel = migraineViewModel.ToMigraineModel();
				//migraineModel.ID = Guid.NewGuid();
				DataContext.Migraines.Add(migraineModel);
				DataContext.SaveChanges();
				return RedirectToAction("Index");
			}

			return View(migraineViewModel);
		}

		#endregion	Create

		#region Edit

		[Route("edit-migraine-{id?}")]
		public ActionResult Edit(string id = "")
		{
			var migraineModel = HelperMethods.FindFromGuid(DataContext, id);

			if (migraineModel == null)
			{
				return HttpNotFound();
			}

			return View(EditableMigraineViewModel.FromMigraineModel(migraineModel));
		}

		[HttpPost]
		[Route("edit-migraine-{id?}")]
		public ActionResult Edit(EditableMigraineViewModel migraineViewModel)
		{
			if (ModelState.IsValid)
			{
				var modelUpdates = migraineViewModel.ToMigraineModel();

				var migraineModel = HelperMethods.FindFromGuid(DataContext, migraineViewModel.ID.ToString());
				migraineModel.Update(modelUpdates);

				DataContext.Entry(migraineModel).State = EntityState.Modified;
				try
				{
					DataContext.SaveChanges();
				}
				catch (Exception)
				{
					DataContext.Entry(migraineModel).Reload();
					DataContext.SaveChanges();
					//DataContext.Refresh(RefreshMode.ClientWins, DataContext.Migraines);
					//DataContext.SaveChanges();
				}
				return RedirectToAction("Index");
			}
			return View(migraineViewModel);
		}

		#endregion	Edit

		#region Delete

		[Route("delete-migraine-{id?}")]
		public ActionResult Delete(string id = "")
		{
			var migraineModel = HelperMethods.FindFromGuid(DataContext, id);

			if (migraineModel == null)
			{
				//return HttpNotFound();
				return Index(); //migraine could not be found to be deleted
			}

			return View(migraineModel);
		}

		[HttpPost, ActionName("Delete")]
		public ActionResult DeleteConfirmed(Guid id)
		{
			MigraineModel migrainemodel = DataContext.Migraines.Find(id);
			DataContext.Migraines.Remove(migrainemodel);
			DataContext.SaveChanges();
			return RedirectToAction("Index");
		} 

		#endregion	Delete


		#region JSON

		[HttpGet]
		public JsonResult GetMigraineEventsFromDateRange(DateTime startDate, DateTime endDate)
		{
			return this.Json(DataContext.Migraines);
			//var migraineModels = DataContext.Migraines.Where(m => MigraineInBounds(
		}


		#endregion	JSON


		#region Private Helper Methods

		private ActionResult ReturnErrorIndexViewIfMissingDate(string date, out DateTime normalizedDate, string message = "You forgot to pick a date")
		{
			if (!DateTime.TryParse(date, out normalizedDate))
			{
				var errorMessage = message;
				return Index(startDate:null,endDate:null, errors: new[] { errorMessage });
			}
			else
			{
				return null;
			}
		}

		#endregion	Private Helper Methods

		protected class HelperMethods
		{
			public static IEnumerable<MigraineModel> GetOrderedMigrainesList(MigraineDiaryMVC_DBContext context)
			{
				try
				{
					var migraines = context.Migraines.OrderBy(m => m.DateStarted).ThenBy(m => m.TimeStarted).ToList();
					return migraines;
				}
				catch (Exception e)
				{
					var connectionString = context.Database.Connection.ConnectionString;
					var errorInfo = CreateInfoFromFullException(e);
					var fullString = String.Format("ConnectionString: [{0}]. \r\n ErrorInfo: [{1}]", connectionString, errorInfo);
					throw new Exception(fullString);
				}
			}

			private static string CreateInfoFromFullException(Exception e, int errorIndex = 0)
			{
				const string headerLine = "-----------------";
				const string separator = "*********************\r\n*********************";

				var stringBuilder = new System.Text.StringBuilder();

				stringBuilder.AppendFormat("Error[{0}]:\r\n", errorIndex).AppendLine(headerLine);
				stringBuilder.AppendLine(e.Message).AppendLine();
				stringBuilder.AppendFormat("Source[{0}]:\r\n", errorIndex).AppendLine(headerLine);
				stringBuilder.AppendLine(e.Source).AppendLine();
				stringBuilder.AppendFormat("Stack Trace[{0}]\r\n", errorIndex).AppendLine(headerLine);
				stringBuilder.AppendLine(e.StackTrace).AppendLine().AppendLine(separator);

				if (e.InnerException != null)
					stringBuilder.AppendLine(CreateInfoFromFullException(e.InnerException, ++errorIndex));

				return stringBuilder.ToString();
			}



			public static MigrainesModel GetMigrainesModelWithErrors(MigraineDiaryMVC_DBContext context, params string[] errors)
			{
				var model = GetBasicMigrainesModel(context);
				model.Errors.AddRange(errors);
				return model;
			}

			public static MigrainesModel GetMigrainesModelWithWarnings(MigraineDiaryMVC_DBContext context, params string[] warnings)
			{
				var model = GetBasicMigrainesModel(context);
				model.Warnings.AddRange(warnings);
				return model;
			}

			public static MigrainesModel GetBasicMigrainesModel(MigraineDiaryMVC_DBContext context)
			{
				var models = GetOrderedMigrainesList(context);
				var model = new MigrainesModel(models);

				return model;
			}

			public static MigraineModel FindFromGuid(MigraineDiaryMVC_DBContext context, string id = "")
			{
				try
				{
					var guid = String.IsNullOrWhiteSpace(id)
						? Guid.Empty
						: Guid.Parse(id);

					var migraineModel = context.Migraines.Find(guid);
					return migraineModel;
				}
				catch (Exception e)
				{
					//that wasn't a valid guid....
					return null;
				}
			}

			public static bool DatesMatch(string left, string right)
			{
				return DatesMatch(DateTime.Parse(left), right);
			}

			public static bool DatesMatch(DateTime left, string right)
			{
				return DatesMatch(left, DateTime.Parse(right));
			}

			public static bool DatesMatch(DateTime left, DateTime right)
			{
				return left == right;
			}

			public static DateTime GetDateFromString(string date, DateTime? defaultDate = null)
			{
				var defaultDateTime = defaultDate ?? DateTime.Today;

				DateTime dt;
				if (string.IsNullOrWhiteSpace(date) || !DateTime.TryParse(date, out dt))
				{
					dt = DateTime.Today;
				}
				return dt;
			}
		}
	}
}