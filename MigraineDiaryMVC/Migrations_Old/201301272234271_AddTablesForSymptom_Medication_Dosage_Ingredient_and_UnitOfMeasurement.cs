namespace MigraineDiaryMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTablesForSymptom_Medication_Dosage_Ingredient_and_UnitOfMeasurement : DbMigration
    {
			public override void Up()
			{
				CreateSymptoms();
				CreateUnitsOfMeasurement();
				CreateDosages();
				CreateIngredients();
				CreateMedications();
			}

			public override void Down()
			{
				DropTable("dbo.SymptomModels");
				DropTable("dbo.MedicationModels");
				DropTable("dbo.IngredientModels");
				DropTable("dbo.DosageModels");
				DropTable("dbo.UnitsOfMeasurementModels");
			}

			private void CreateSymptoms()
			{
				CreateTable(
						"dbo.SymptomModels",
						c => new
						{
							ID = c.Int(nullable: false),
							Name = c.String(nullable: false),
						})
						.PrimaryKey(t => t.ID);
			}

			private void CreateUnitsOfMeasurement()
			{
				CreateTable(
						"dbo.UnitOfMeasurementModels",
						c => new
						{
							ID = c.Int(nullable: false),
							Name = c.String(nullable: false),
						})
						.PrimaryKey(t => t.ID);
			}

			private void CreateDosages()
			{
				CreateTable(
						"dbo.DosageModels",
						c => new
						{
							ID = c.Int(nullable: false),
							Quantity = c.Double(nullable: false),
							UnitOfMeasurementID = c.Int(nullable: false),
						})
						.PrimaryKey(t => t.ID)
						.ForeignKey("dbo.UnitOfMeasurementModels", t => t.UnitOfMeasurementID);
			}

			private void CreateIngredients()
			{
				CreateTable(
						"dbo.IngredientModels",
						c => new
						{
							ID = c.Int(nullable: false),
							Name = c.String(nullable: false),
							DosageID = c.Int(nullable: true),
						})
						.PrimaryKey(t => t.ID)
						.ForeignKey("dbo.DosageModels", t => t.DosageID);
			}

			private void CreateMedications()
			{
				CreateTable(
						"dbo.MedicationModels",
						c => new
						{
							ID = c.Int(nullable: false),
							Name = c.String(nullable: false),
							DosageID = c.Int(nullable: true),
							IntredientID = c.Int(nullable: true),
						})
						.PrimaryKey(t => t.ID)
						.ForeignKey("dbo.DosageModels", t => t.DosageID)
						.ForeignKey("dbo.IngredientModels", t => t.IntredientID);
			}

    }
}
