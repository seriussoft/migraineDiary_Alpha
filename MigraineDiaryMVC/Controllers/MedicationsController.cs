using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using SeriusSoft.MigraineDiaryMVC.MigraineModels;
using MigraineDiaryMVC.Controllers.Extensions;
using MigraineDiaryMVC.Models;

namespace MigraineDiaryMVC.Controllers
{
	//[RoutePrefix("Medications"), Route("Index")]
	[Authorize(Roles = RoleConstants.Customer)]
	public class MedicationsController : BaseDisposableMigraineController
	{
		public MedicationsController()
		{
			SetupDosageRepository();
		}

		[Route("~/Medications/Index")]
		[Route("~/Medications/")]
		public ActionResult Index()
		{
			var medications = DataContext.Medications.Include(m => m.Dosage).Include(m => m.Ingredients);
			ViewBag.Ingredients = DataContext.Ingredients.ToList();
			return View(medications.ToList());
		}

		public ActionResult Details(int id = 0)
		{
			MedicationModel medicationmodel = DataContext.Medications.Find(id);
			if (medicationmodel == null)
			{
				return HttpNotFound();
			}
			return View(medicationmodel);
		}

		public ActionResult Create()
		{
			ViewBag.DosageID = this.OrderedDosageSelectList();
			ViewBag.Ingredients = this.OrderedIngredientsMultiSelectList();

			return View();
		}

		[HttpPost]
		public ActionResult Create(MedicationModel medicationmodel)
		{
			if (ModelState.IsValid)
			{
				var ingredients = this.GetSelectedIngredientsFromIds(medicationmodel.IngredientIDs);
				medicationmodel.Ingredients = this.AttachIngredientsToDatabaseAndReturn(DataContext.Ingredients, ingredients);

				DataContext.Medications.Add(medicationmodel);
				DataContext.SaveChanges();
				return RedirectToAction("Index");
			}

			ViewBag.DosageID = this.OrderedDosageSelectList(medicationmodel.DosageID);
			ViewBag.Ingredients = this.OrderedIngredientsMultiSelectList();

			return View(medicationmodel);
		}

		public ActionResult Edit(int id = 0)
		{
			var medicationmodel = DataContext.Medications.Include(m => m.Dosage).Include(m => m.Ingredients).First(m => m.MedicationID == id);
			if (medicationmodel == null)
			{
				return HttpNotFound();
			}

			medicationmodel.IngredientIDs = medicationmodel.Ingredients.Select(i => i.IngredientID).ToList();
			
			ViewBag.Dosages = this.OrderedDosageSelectList(medicationmodel.DosageID);
			ViewBag.Ingredients = this.OrderedIngredientsMultiSelectList(medicationmodel.IngredientIDs.ToList());

			return View(medicationmodel);
		}

		//
		// POST: /Medications/Edit/5

		[HttpPost]
		public ActionResult Edit(MedicationModel medicationmodel)
		{
			if (ModelState.IsValid)
			{
				//get the true model
				var model = DataContext.Medications.Include(m => m.Ingredients).Single(m => m.MedicationID == medicationmodel.MedicationID);
				
				//set all scalar values and then clear out the list of items currently attached
				DataContext.Entry(model).CurrentValues.SetValues(medicationmodel);
				model.Ingredients.Clear();

				// disable autodetectchanges before runing loop with DbSet<T>.Find because it slows it down. similar to how you should tell a gridview to not draw or fire events until everything is loaded.
				// we then load up all the true ingredients based on the incoming list of ingredientIDs
				// finally, we turn the autodetectchangesenabled back on
				DataContext.Configuration.AutoDetectChangesEnabled = false;
				foreach (var ingredientID in medicationmodel.IngredientIDs)
				{
					var ingredient = DataContext.Ingredients.Find(ingredientID);
					model.Ingredients.Add(ingredient);
				}
				DataContext.Configuration.AutoDetectChangesEnabled = true;

				//save changes and redirect to the medications index
				DataContext.SaveChanges();
				return RedirectToAction("Index");
			}

			medicationmodel.IngredientIDs = medicationmodel.Ingredients.Select(i => i.IngredientID).ToList();
			
			ViewBag.Dosages = this.OrderedDosageSelectList(medicationmodel.DosageID);
			ViewBag.Ingredients = this.OrderedIngredientsMultiSelectList(medicationmodel.IngredientIDs.ToList());

			return View(medicationmodel);
		}

		//
		// GET: /Medications/Delete/5

		public ActionResult Delete(int id = 0)
		{
			MedicationModel medicationmodel = DataContext.Medications.Find(id);
			if (medicationmodel == null)
			{
				return HttpNotFound();
			}
			return View(medicationmodel);
		}

		//
		// POST: /Medications/Delete/5

		[HttpPost, ActionName("Delete")]
		public ActionResult DeleteConfirmed(int id)
		{
			var medicationmodel = DataContext.Medications.Include(m => m.Ingredients).Single(m => m.MedicationID == id);
			
			//remove any *-* relationship pathways
			medicationmodel.Ingredients.Clear();
			DataContext.SaveChanges();

			//now it's safe to remove this item
			DataContext.Medications.Remove(medicationmodel);
			DataContext.SaveChanges();
			return RedirectToAction("Index");
		}
	}
}