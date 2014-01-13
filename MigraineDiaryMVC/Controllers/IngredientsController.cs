using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using SeriusSoft.MigraineDiaryMVC.MigraineModels;
using SeriusSoft.MigraineDiaryMVC.MigraineRepository;
using MigraineDiaryMVC.Controllers.Extensions;
using SeriusSoft.MigraineDiaryMVC.MigraineRepository.Repositories;
using MigraineDiaryMVC.Models;

namespace MigraineDiaryMVC.Controllers
{
	[Authorize(Roles = RoleConstants.Customer)]
	public class IngredientsController : BaseDisposableMigraineController
	{
		public IngredientsController()
		{
			SetupDosageRepository();
		}

		[Route("~/Ingredients/")]
		public ActionResult Index()
		{
			var ingredients = DataContext.Ingredients.Include(i => i.Dosage);
			return View(ingredients.ToList());
		}

		public ActionResult Details(int id = 0)
		{
			IngredientModel ingredientmodel = DataContext.Ingredients.Find(id);
			if (ingredientmodel == null)
			{
				return HttpNotFound();
			}
			return View(ingredientmodel);
		}


		public ActionResult Create()
		{
			ViewBag.DosageID = this.OrderedDosageSelectList();//new SelectList(this.OrderedDosageList(), "DosageID", "Display");
			return View();
		}


		[HttpPost]
		public ActionResult Create(IngredientModel ingredientmodel)
		{
			if (ModelState.IsValid)
			{
				DataContext.Ingredients.Add(ingredientmodel);
				DataContext.SaveChanges();
				return RedirectToAction("Index");
			}

			ViewBag.DosageID = this.OrderedDosageSelectList(ingredientmodel.DosageID);//new SelectList(this.OrderedDosageList(), "DosageID", "Display", ingredientmodel.DosageID);
			return View(ingredientmodel);
		}

		public ActionResult Edit(int id = 0)
		{
			IngredientModel ingredientmodel = DataContext.Ingredients.Find(id);
			if (ingredientmodel == null)
			{
				return HttpNotFound();
			}
			ViewBag.Dosages = new SelectList(this.OrderedDosageList(), "DosageID", "Display", ingredientmodel.DosageID);
			
			return View(ingredientmodel);
		}

		[HttpPost]
		public ActionResult Edit(IngredientModel ingredientmodel)
		{
			if (ModelState.IsValid)
			{
				DataContext.Entry(ingredientmodel).State = EntityState.Modified;
				DataContext.SaveChanges();
				return RedirectToAction("Index");
			}
			ViewBag.Dosages = new SelectList(this.OrderedDosageList(), "DosageID", "Display", ingredientmodel.DosageID);
			return View(ingredientmodel);
		}

		public ActionResult Delete(int id = 0)
		{
			IngredientModel ingredientmodel = DataContext.Ingredients.Find(id);
			if (ingredientmodel == null)
			{
				return HttpNotFound();
			}
			return View(ingredientmodel);
		}

		//
		// POST: /Ingredients/Delete/5

		[HttpPost, ActionName("Delete")]
		public ActionResult DeleteConfirmed(int id)
		{
			IngredientModel ingredientmodel = DataContext.Ingredients.Find(id);
			DataContext.Ingredients.Remove(ingredientmodel);
			DataContext.SaveChanges();
			return RedirectToAction("Index");
		}
	}
}