namespace MigraineDiaryMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTimeColumns : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MigraineModels", "TimeStarted", c => c.DateTime(nullable: false));
            AddColumn("dbo.MigraineModels", "TimeEnded", c => c.DateTime(nullable: false));
            DropColumn("dbo.MigraineModels", "LengthOfMigraine");
        }
        
        public override void Down()
        {
            AddColumn("dbo.MigraineModels", "LengthOfMigraine", c => c.Time(nullable: false));
            DropColumn("dbo.MigraineModels", "TimeEnded");
            DropColumn("dbo.MigraineModels", "TimeStarted");
        }
    }
}
