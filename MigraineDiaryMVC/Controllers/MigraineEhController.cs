using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MigraineDiaryMVC.Models;
using MigraineDiaryMVC.ViewModels;
using SeriusSoft.MigraineDiaryMVC.MigraineModels;

namespace MigraineDiaryMVC.Controllers
{
	[Authorize(Roles = RoleConstants.Customer)]
	public class MigraineEhController : Controller
	{

		private static MigraineViewModel MigrainesWrapper;

		static MigraineEhController()
		{
			MigrainesWrapper = new MigraineViewModel();
		}

		//
		// GET: /Migraine/

		public ActionResult Index()
		{
			return View(MigrainesWrapper.Migraines);
		}

		//
		// GET: /Migraine/Details/5

		public ActionResult Details(int id)
		{
			return View();
		}

		public ActionResult MigraineAdd()
		{
			return View();
		}

		public ActionResult MigrainAdd(MigraineModel migrainModel)
		{
			MigrainesWrapper.CreateMigraine(migrainModel);
			return View();
		}

		////
		//// GET: /Migraine/Create

		//public ActionResult Create()
		//{
		//	return View();
		//}

		////
		//// POST: /Migraine/Create

		//[HttpPost]
		//public ActionResult Create(FormCollection collection)
		//{
		//	try
		//	{
		//		// TODO: Add insert logic here

		//		return RedirectToAction("Index");
		//	}
		//	catch
		//	{
		//		return View();
		//	}
		//}

		//
		// GET: /Migraine/Edit/5

		public ActionResult Edit(int id)
		{
			return View();
		}

		//
		// POST: /Migraine/Edit/5

		[HttpPost]
		public ActionResult Edit(int id, FormCollection collection)
		{
			try
			{
				// TODO: Add update logic here

				return RedirectToAction("Index");
			}
			catch
			{
				return View();
			}
		}

		//
		// GET: /Migraine/Delete/5

		public ActionResult Delete(int id)
		{
			return View();
		}

		//
		// POST: /Migraine/Delete/5

		[HttpPost]
		public ActionResult Delete(int id, FormCollection collection)
		{
			try
			{
				// TODO: Add delete logic here

				return RedirectToAction("Index");
			}
			catch
			{
				return View();
			}
		}
	}
}
