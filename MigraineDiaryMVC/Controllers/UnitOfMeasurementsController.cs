using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using MigraineDiaryMVC.Models;
using SeriusSoft.MigraineDiaryMVC.MigraineModels;
using SeriusSoft.MigraineDiaryMVC.MigraineRepository;

namespace MigraineDiaryMVC.Controllers
{
	[Authorize(Roles = RoleConstants.Customer)]
	public class UnitOfMeasurementsController : BaseDisposableMigraineController
	{
		[Route("~/UnitOfMeasurements")]
		public async Task<ActionResult> Index()
		{
			var userID = base.GetCurrentUserID();
			var unitsOfMeasurement = DataContext.UnitsOfMeasurement.WhereBoundToUser(userID);//.Where(uom => uom.UserID == userID);
			return View(await unitsOfMeasurement.ToListAsync());
		}

		public ActionResult Details(int id = 0)
		{
			UnitOfMeasurementModel unitofmeasurementmodel = DataContext.UnitsOfMeasurement.Find(id);
			if (unitofmeasurementmodel == null)
			{
				return HttpNotFound();
			}
			return View(unitofmeasurementmodel);
		}

		public ActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public async Task<ActionResult> Create(UnitOfMeasurementModel unitofmeasurementmodel)
		{
			if (ModelState.IsValid)
			{
				await Task.Run(() =>
				{
					unitofmeasurementmodel.UserID = base.GetCurrentUserID();
					DataContext.UnitsOfMeasurement.Add(unitofmeasurementmodel);
					DataContext.SaveChanges();
				});
				return RedirectToAction("Index");
			}

			return View(unitofmeasurementmodel);
		}

		public ActionResult Edit(int id = 0)
		{
			var unitofmeasurementmodel = DataContext.UnitsOfMeasurement.Find(id);
			if (unitofmeasurementmodel == null)
			{
				return HttpNotFound();
			}
			return View(unitofmeasurementmodel);
		}

		[HttpPost]
		public async Task<ActionResult> Edit(UnitOfMeasurementModel unitofmeasurementmodel)
		{
			if (ModelState.IsValid)
			{
				await Task.Run(() =>
				{
					DataContext.Entry(unitofmeasurementmodel).State = EntityState.Modified;
					unitofmeasurementmodel.UserID = base.GetCurrentUserID();
					DataContext.SaveChanges();
				});
				return RedirectToAction("Index");
			}
			return View(unitofmeasurementmodel);
		}

		public ActionResult Delete(int id = 0)
		{
			UnitOfMeasurementModel unitofmeasurementmodel = DataContext.UnitsOfMeasurement.Find(id);
			if (unitofmeasurementmodel == null)
			{
				return HttpNotFound();
			}
			return View(unitofmeasurementmodel);
		}

		[HttpPost, ActionName("Delete")]
		public ActionResult DeleteConfirmed(int id)
		{
			UnitOfMeasurementModel unitofmeasurementmodel = DataContext.UnitsOfMeasurement.Find(id);
			DataContext.UnitsOfMeasurement.Remove(unitofmeasurementmodel);
			DataContext.SaveChanges();
			return RedirectToAction("Index");
		}
	}
}