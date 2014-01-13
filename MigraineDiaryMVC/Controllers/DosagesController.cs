using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//using MigraineDiaryMVC.Models;

using SeriusSoft.MigraineDiaryMVC.MigraineRepository;
using SeriusSoft.MigraineDiaryMVC.MigraineModels;
using SeriusSoft.MigraineDiaryMVC.MigraineRepository.Repositories;
using SeriusSoft.MigraineDiaryMVC.MigraineRepository.Interfaces;
using MigraineDiaryMVC.Models;

namespace MigraineDiaryMVC.Controllers
{
	[Authorize(Roles = RoleConstants.Customer)]
	public class DosagesController : BaseMigraineController
	{
		public DosagesController()
		{
			SetupDosageRepository();
			//this.Repository = new DosageRepository(this.DataContext);

		}

		public DosagesController(IDosageRepository dosageRepository)
		{
			this.DosageRepository = dosageRepository;
		}

		[Route("Dosages")]
		public ActionResult Index()
		{
			var dosage = this.DosageRepository.GetDoses();
			//var dosage = DB.Dosage.Include(d => d.UnitOfMeasurement);
			return View(dosage.ToList());
		}

		public ActionResult Details(int id = 0)
		{
			var dosageModel = this.DosageRepository.GetDoseByID(id);
			//DosageModel dosagemodel = DB.Dosage.Find(id);
			if (dosageModel == null)
			{
				return HttpNotFound();
			}
			return View(dosageModel);
		}

		public ActionResult Create()
		{
			var dosageModel = new DosageModel();
			ViewBag.UnitOfMeasurementID = new SelectList(this.DataContext.UnitsOfMeasurement, "UnitOfMeasurementID", "Name", dosageModel.UnitOfMeasurementID);
			return View();
		}

		[HttpPost]
		public ActionResult Create(DosageModel dosageModel)
		{
			if (this.DosageRepository.CreateDoseIfValid(dosageModel, ModelState.IsValid))
			{
				return RedirectToAction("Index");
			}

			ViewBag.UnitOfMeasurementID = new SelectList(this.DataContext.UnitsOfMeasurement, "UnitOfMeasurementID", "Name", dosageModel.UnitOfMeasurementID);
			return View(dosageModel);
		}

		public ActionResult Edit(int id = 0)
		{
			var dosageModel = this.DosageRepository.GetDoseByID(id);
			if (dosageModel == null)
			{
				return HttpNotFound();
			}
			ViewBag.UnitOfMeasurements = new SelectList(this.DataContext.UnitsOfMeasurement, "UnitOfMeasurementID", "Name", dosageModel.UnitOfMeasurementID);
			return View(dosageModel);
		}

		[HttpPost]
		public ActionResult Edit(DosageModel dosageModel)
		{
			if (this.DosageRepository.UpdateDoseIfValid(dosageModel, ModelState.IsValid))
			{
				return RedirectToAction("Index");
			}

			//if (ModelState.IsValid)
			//{
			//	DB.Entry(dosagemodel).State = EntityState.Modified;
			//	DB.SaveChanges();
			//	return RedirectToAction("Index");
			//}
			ViewBag.UnitOfMeasurements = new SelectList(this.DataContext.UnitsOfMeasurement, "UnitOfMeasurementID", "Name", dosageModel.UnitOfMeasurementID);
			return View(dosageModel);
		}

		public ActionResult Delete(int id = 0)
		{
			var dosageModel = this.DosageRepository.GetDoseByID(id);
			//DosageModel dosagemodel = DB.Dosage.Find(id);
			if (dosageModel == null)
			{
				return HttpNotFound();
			}
			return View(dosageModel);
		}

		[HttpPost, ActionName("Delete")]
		public ActionResult DeleteConfirmed(int id)
		{
			this.DosageRepository.DeleteDose(id);
			return RedirectToAction("Index");
		}

		protected override void Dispose(bool disposing)
		{
			((IDisposable)this.DosageRepository).Dispose();
			base.Dispose(disposing);
		}
	}
}