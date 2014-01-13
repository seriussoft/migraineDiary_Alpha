namespace MigraineDiaryMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RolesAndUserIDForUnitsOfMeasurement : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UnitOfMeasurementModels", "UserID", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.UnitOfMeasurementModels", "UserID");
        }
    }
}
