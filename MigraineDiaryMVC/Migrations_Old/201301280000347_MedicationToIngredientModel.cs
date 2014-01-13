namespace MigraineDiaryMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MedicationToIngredientModel : DbMigration
    {
        public override void Up()
        {
					UpdateIDColumns();
        }

				private void UpdateIDColumns()
				{
					AlterColumn("dbo.UnitOfMeasurementModels", "ID", c => c.Int(nullable: false, name: "UnitOfMeasurementID"));
					AlterColumn("dbo.DosageModels", "ID", c => c.Int(nullable: false, name: "DosageID"));
					AlterColumn("dbo.MedicationModels", "ID", c => c.Int(nullable: false, name: "MedicationID"));
					AlterColumn("dbo.IngredientModels", "ID", c => c.Int(nullable: false, name: "IngredientID"));
				}
        
        public override void Down()
        {
					AlterColumn("dbo.UnitOfMeasurementModels", "UnitOfMeasurementID", c => c.Int(nullable: false, name: "ID"));
					AlterColumn("dbo.DosageModels", "DosageID", c => c.Int(nullable: false, name: "ID"));
					AlterColumn("dbo.MedicationModels", "MedicationID", c => c.Int(nullable: false, name: "ID"));
					AlterColumn("dbo.IngredientModels", "IngredientID", c => c.Int(nullable: false, name: "ID"));
        }
    }
}
