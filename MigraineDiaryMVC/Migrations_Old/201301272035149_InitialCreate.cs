namespace MigraineDiaryMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MigraineModels",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        DateStarted = c.DateTime(nullable: false),
                        LengthOfMigraine = c.Time(nullable: false),
                        Severity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.MigraineModels");
        }
    }
}
