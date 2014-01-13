using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using SeriusSoft.MigraineDiaryMVC.MigraineModels;

namespace MigraineDiaryMVC.Controllers.Extensions
{
	public static class ControllerExtensions
	{
		#region Dosage Helpers
		public static IEnumerable OrderedDosageList(this BaseMigraineController controller)
		{
			var dosageList = controller.DosageRepository.GetDoses();
			return dosageList.OrderBy(d => d.UnitOfMeasurement.Name).ThenBy(d => d.Quantity).ToList();

			//var dosageList = controller.DataContext.Dosage.ToList();
			//return dosageList.OrderBy(d => d.UnitOfMeasurement.Name).ThenBy(d => d.Quantity).ToList();
		}

		public static SelectList OrderedDosageSelectList(this BaseMigraineController controller, object selectedValue = null)
		{
			if (selectedValue != null)
			{
				return new SelectList(controller.OrderedDosageList(), "DosageID", "Display", selectedValue);
			}
			else
			{
				return new SelectList(controller.OrderedDosageList(), "DosageID", "Display");
			}
		} 
		#endregion

		#region Ingredient Helpers
		public static IEnumerable OrderedIngredientList(this BaseMigraineController controller)
		{
			var ingredientList = controller.DataContext.Ingredients.ToList();
			return ingredientList.OrderBy(i => i.Name).ThenBy(i => i.Dosage.UnitOfMeasurement.Name).ThenBy(i => i.Dosage.Quantity).ToList();
		}

		public static IEnumerable<IngredientModel> GetSelectedIngredientsFromIds(this BaseMigraineController controller, List<int> selectedIngredientIDs)
		{
			if (selectedIngredientIDs == null || !selectedIngredientIDs.Any())
			{
				return new List<IngredientModel>();
			}

			var fullIngredientList = controller.DataContext.Ingredients;

			var ingredients =
					from ingredient in fullIngredientList
					join ingredientID in selectedIngredientIDs on ingredient.IngredientID equals ingredientID
					select ingredient;

			return ingredients;
		}

		public static List<T> AttachIngredientsToDatabaseAndReturn<T>(this BaseMigraineController controller, DbSet<T> dbSet, IEnumerable<T> selectedItemsList) where T : class
		{
			var listOfItemsForModel = new List<T>();

			//foreach (var item in dbSet.Except(selectedItemsList).ToList())
			//{
			//	dbSet.Remove(item);
			//}

			foreach (var item in selectedItemsList)
			{
				dbSet.Attach(item);
				listOfItemsForModel.Add(item);
			}

			return listOfItemsForModel;
		}


		public static MultiSelectList OrderedIngredientsMultiSelectList(this BaseMigraineController controller, IEnumerable<int> selectedValues = null)
		{
			if (selectedValues != null && selectedValues.Any())
			{
				//var selected =
				//	from i in controller.DataContext.Ingredients
				//	join iSelected in selectedValues on i.IngredientID equals iSelected.IngredientID
				//	select i;

				return new MultiSelectList(controller.OrderedIngredientList(), "IngredientID", "Display", selectedValues.ToList());//selected.ToList());
			}
			else
			{
				return new MultiSelectList(controller.OrderedIngredientList(), "IngredientID", "Display");
			}
		}
		#endregion

	} 
		

	public static class IEnumerableExtensions
	{
		public static bool Any(this IEnumerable list)
		{
			return list != null && list.GetEnumerator().MoveNext();
		}
	}
}