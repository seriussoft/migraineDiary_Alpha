using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MigraineDiaryMVC.Models;
using SeriusSoft.MigraineDiaryMVC.MigraineModels;

namespace MigraineDiaryMVC.Controllers
{
	[Authorize(Roles = RoleConstants.Customer)]
	public class SymptomsController : BaseDisposableMigraineController
	{
		//private MigraineDiaryMVC_DBContext db = new MigraineDiaryMVC_DBContext();

		//
		// GET: /Symptoms/

		[Route("~/Symptoms")]
		public ActionResult Index()
		{
			return View(DataContext.Symptoms.ToList());
		}

		//
		// GET: /Symptoms/Details/5

		public ActionResult Details(int id = 0)
		{
			SymptomModel symptommodel = DataContext.Symptoms.Find(id);
			if (symptommodel == null)
			{
				return HttpNotFound();
			}
			return View(symptommodel);
		}

		//
		// GET: /Symptoms/Create

		public ActionResult Create()
		{
			return View();
		}

		//
		// POST: /Symptoms/Create

		[HttpPost]
		public ActionResult Create(SymptomModel symptommodel)
		{
			if (ModelState.IsValid)
			{
				DataContext.Symptoms.Add(symptommodel);
				DataContext.SaveChanges();
				return RedirectToAction("Index");
			}

			return View(symptommodel);
		}

		//
		// GET: /Symptoms/Edit/5

		public ActionResult Edit(int id = 0)
		{
			SymptomModel symptommodel = DataContext.Symptoms.Find(id);
			if (symptommodel == null)
			{
				return HttpNotFound();
			}
			return View(symptommodel);
		}

		//
		// POST: /Symptoms/Edit/5

		[HttpPost]
		public ActionResult Edit(SymptomModel symptommodel)
		{
			if (ModelState.IsValid)
			{
				DataContext.Entry(symptommodel).State = EntityState.Modified;
				DataContext.SaveChanges();
				return RedirectToAction("Index");
			}
			return View(symptommodel);
		}

		//
		// GET: /Symptoms/Delete/5

		public ActionResult Delete(int id = 0)
		{
			SymptomModel symptommodel = DataContext.Symptoms.Find(id);
			if (symptommodel == null)
			{
				return HttpNotFound();
			}
			return View(symptommodel);
		}

		//
		// POST: /Symptoms/Delete/5

		[HttpPost, ActionName("Delete")]
		public ActionResult DeleteConfirmed(int id)
		{
			SymptomModel symptommodel = DataContext.Symptoms.Find(id);
			DataContext.Symptoms.Remove(symptommodel);
			DataContext.SaveChanges();
			return RedirectToAction("Index");
		}
	}
}