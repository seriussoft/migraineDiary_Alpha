#define BYPASS_DEBUG

namespace MigraineDiaryMVC.Migrations.Migraines
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel;
	using System.Data.Entity;
	using System.Data.Entity.Migrations;
	using System.Linq;
	using SeriusSoft.MigraineDiaryMVC.MigraineModels;

	//using Models = SeriusSoft.MigraineDiaryMVC.MigraineModels;

	internal sealed class MigraineMigrationConfiguration : DbMigrationsConfiguration<SeriusSoft.MigraineDiaryMVC.MigraineRepository.MigraineDiaryMVC_DBContext>
	{
		public MigraineMigrationConfiguration()
		{
			//AutomaticMigrationsEnabled = false;
			AutomaticMigrationsEnabled = true;
			//AutomaticMigrationDataLossAllowed = true;
		}

		protected override void Seed(SeriusSoft.MigraineDiaryMVC.MigraineRepository.MigraineDiaryMVC_DBContext context)
		{
			//http://www.asp.net/mvc/tutorials/getting-started-with-ef-using-mvc/migrations-and-deployment-with-the-entity-framework-in-an-asp-net-mvc-application
			//  This method will be called after migrating to the latest version.

			//always call the release method. this will populate the db with the required 'default' values that should be there no matter what
			CallReleaseSeed(context);

#if DEBUG || BYPASS_DEBUG
			CallDebugSeed(context);
#endif
		}

		private void CallReleaseSeed(SeriusSoft.MigraineDiaryMVC.MigraineRepository.MigraineDiaryMVC_DBContext context)
		{
			var testDataMaker = new TestDataCreator();

			//add or update the default trigger types
			var triggerTypes = testDataMaker.CreateTriggerTypes();
			triggerTypes.ForEach(t => context.TriggerTypes.AddOrUpdate(tInner => tInner.Name, t));
			context.SaveChanges();
		}

		private void CallDebugSeed(SeriusSoft.MigraineDiaryMVC.MigraineRepository.MigraineDiaryMVC_DBContext context)
		{
			var testDataMaker = new TestDataCreator();

			//symptoms
			var symptoms = testDataMaker.CreateSymptoms();
			symptoms.ForEach(s => context.Symptoms.AddOrUpdate(sInner => sInner.Name, s));
			context.SaveChanges();

			//unitsOfMeasurement
			var unitsOfMeasurement = testDataMaker.CreateUnitsOfMeasurement();
			unitsOfMeasurement.ForEach(u => context.UnitsOfMeasurement.AddOrUpdate(uInner => uInner.Name, u));
			context.SaveChanges();

			//dosages
			var dosages = testDataMaker.CreateDosages(context.UnitsOfMeasurement);
			dosages.ForEach(d => context.Dosage.AddOrUpdate(dInner => new { dInner.Quantity, dInner.UnitOfMeasurementID }, d));
			context.SaveChanges();

			////ingredients
			var ingredients = testDataMaker.CreateIngredients();
			ingredients.ForEach(i => context.Ingredients.AddOrUpdate(iInner => iInner.Display, i));
			context.SaveChanges();

			////medications
			var medications = testDataMaker.CreateMedications();
			medications.ForEach(m => context.Medications.AddOrUpdate(mInner => new { mInner.Name, (mInner.Dosage ?? new DosageModel()).Display }, m));
			context.SaveChanges();

			////triggers
			var triggers = testDataMaker.CreateTriggers(context.TriggerTypes);
			triggers.ForEach(t => context.Triggers.AddOrUpdate(tInner => tInner.Name, t));
			context.SaveChanges();

			////migraines
			var migraines = testDataMaker.CreateMigraines();
			migraines.ForEach(m => context.Migraines.AddOrUpdate(mInner => new { mInner.DateStarted, mInner.TimeStarted, mInner.TimeEnded }, m));
			context.SaveChanges();
		}

		#region Data Creators



		#endregion	Data Creators
	}

	public class TestDataCreator
	{
		public List<SymptomModel> CreateSymptoms()
		{
			var symptoms = new List<SymptomModel>()
			{
				new SymptomModel() { Name = "Sensitivity to light" },
				new SymptomModel() { Name = "Sensitivity to Smell" },
				new SymptomModel() { Name = "Sensitivity to Sound" },
				new SymptomModel() { Name = "Shaky" },
				new SymptomModel() { Name = "Soreness" },
				new SymptomModel() { Name = "Crave Chocolate / Sweets" },
				new SymptomModel() { Name = "Crave fatty/bready foods" },
				new SymptomModel() { Name = "Crave Meat" },
				new SymptomModel() { Name = "Drowsy" },
				new SymptomModel() { Name = "Difficulty Concentrating/Focusing" },
				new SymptomModel() { Name = "Difficulty Speaking" },
				new SymptomModel() { Name = "Energetic" },
				new SymptomModel() { Name = "Eye Pain" },
				new SymptomModel() { Name = "Fatigue" },
				new SymptomModel() { Name = "Irritable" },
				new SymptomModel() { Name = "Lower Back Pain" },
				new SymptomModel() { Name = "Nausea" },
				new SymptomModel() { Name = "Neck Pain" },
				new SymptomModel() { Name = "Numb/Tingly Pinkie (Left)" },
				new SymptomModel() { Name = "Numb/Tingly Pinkie (Right)" },
				new SymptomModel() { Name = "Pulsing" },
				new SymptomModel() { Name = "Talkative" },
				new SymptomModel() { Name = "Thirsty" },
				new SymptomModel() { Name = "Upper Back Pain" },
				new SymptomModel() { Name = "Vomiting" },
				new SymptomModel() { Name = "Weakness" }
			};

			return symptoms;
		}

		public List<UnitOfMeasurementModel> CreateUnitsOfMeasurement()
		{
			var unitsOfMeasurement = new List<UnitOfMeasurementModel>()
			{
				new UnitOfMeasurementModel() { Name = "mg" },
				new UnitOfMeasurementModel() { Name = "g" },
				new UnitOfMeasurementModel() { Name = "oz" },
				new UnitOfMeasurementModel() { Name = "tbsp" },
				new UnitOfMeasurementModel() { Name = "tsp" },
				new UnitOfMeasurementModel() { Name = "%" },
				new UnitOfMeasurementModel() { Name = "pill (small)" },
				new UnitOfMeasurementModel() { Name = "pill (large)" },
				new UnitOfMeasurementModel() { Name = "ml" }
			};

			return unitsOfMeasurement;
		}

		public List<DosageModel> CreateDosages(IEnumerable<UnitOfMeasurementModel> unitsOfMeasurement)
		{
			var units = unitsOfMeasurement.ToList();
			var dosages = new List<DosageModel>()
			{
				new DosageModel() { Quantity = 500, UnitOfMeasurementID = units.Single(u => u.Name == "mg").UnitOfMeasurementID },
				new DosageModel() { Quantity = 1, UnitOfMeasurementID = units.Single(u => u.Name == "pill (small)").UnitOfMeasurementID },
				new DosageModel() { Quantity = 1, UnitOfMeasurementID = units.Single(u => u.Name == "pill (large)").UnitOfMeasurementID },
				new DosageModel() { Quantity = 16, UnitOfMeasurementID = units.Single(u => u.Name == "oz").UnitOfMeasurementID },
				new DosageModel() { Quantity = 750, UnitOfMeasurementID = units.Single(u => u.Name == "mg").UnitOfMeasurementID },
				new DosageModel() { Quantity = 350, UnitOfMeasurementID = units.Single(u => u.Name == "mg").UnitOfMeasurementID },
				new DosageModel() { Quantity = 250, UnitOfMeasurementID = units.Single(u => u.Name == "mg").UnitOfMeasurementID },
				new DosageModel() { Quantity = 200, UnitOfMeasurementID = units.Single(u => u.Name == "mg").UnitOfMeasurementID },
				new DosageModel() { Quantity = 65, UnitOfMeasurementID = units.Single(u => u.Name == "mg").UnitOfMeasurementID },
				new DosageModel() { Quantity = 2, UnitOfMeasurementID = units.Single(u => u.Name == "tbsp").UnitOfMeasurementID },
				new DosageModel() { Quantity = 2, UnitOfMeasurementID = units.Single(u => u.Name == "tsp").UnitOfMeasurementID },
				new DosageModel() { Quantity = 4, UnitOfMeasurementID = units.Single(u => u.Name == "tsp").UnitOfMeasurementID },
				new DosageModel() { Quantity = 10, UnitOfMeasurementID = units.Single(u => u.Name == "ml").UnitOfMeasurementID },
				new DosageModel() { Quantity = 20, UnitOfMeasurementID = units.Single(u => u.Name == "ml").UnitOfMeasurementID },
				new DosageModel() { Quantity = 2, UnitOfMeasurementID = units.Single(u => u.Name == "pill (small)").UnitOfMeasurementID },
				new DosageModel() { Quantity = 2, UnitOfMeasurementID = units.Single(u => u.Name == "pill (large)").UnitOfMeasurementID },
				new DosageModel() { Quantity = 0.5, UnitOfMeasurementID = units.Single(u => u.Name == "pill (large)").UnitOfMeasurementID },
				new DosageModel() { Quantity = 1.5, UnitOfMeasurementID = units.Single(u => u.Name == "pill (large)").UnitOfMeasurementID }
			};

			return dosages;
		}

		[Obsolete("CreateIngredients is not yet populating test data", false)]
		public List<IngredientModel> CreateIngredients()
		{
			var ingredients = new List<IngredientModel>()
			{

			};

			return ingredients;
		}

		[Obsolete("CreateMedications is not yet populating test data", false)]
		public List<MedicationModel> CreateMedications()
		{
			var medications = new List<MedicationModel>()
			{

			};

			return medications;
		}

		public List<TriggerTypeModel> CreateTriggerTypes()
		{
			var triggerTypes = new List<TriggerTypeModel>()
			{
				new TriggerTypeModel() { Name = "Potential Trigger" },
				new TriggerTypeModel() { Name = "Known Trigger" }
			};

			return triggerTypes;
		}

		[Obsolete("CreateTriggers is not yet populating test data", false)]
		public List<TriggerModel> CreateTriggers(IEnumerable<TriggerTypeModel> triggerTypes)
		{
			var types = triggerTypes.ToList();
			var triggers = new List<TriggerModel>()
			{

			};

			return triggers;
		}

		[Obsolete("CreateMigraines is not yet populating test data", false)]
		public List<MigraineModel> CreateMigraines()
		{
			var migraines = new List<MigraineModel>()
			{

			};

			return migraines;
		}
	}
}
