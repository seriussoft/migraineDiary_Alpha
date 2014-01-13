namespace MigraineDiaryMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTriggers : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TriggerModels",
                c => new
                    {
                        TriggerID = c.Int(nullable: false, identity: true),
                        TriggerTypeID = c.Int(nullable: false),
                        Name = c.String(nullable: false),
                        Info = c.String(),
                    })
                .PrimaryKey(t => t.TriggerID)
                .ForeignKey("dbo.TriggerTypeModels", t => t.TriggerTypeID, cascadeDelete: true)
                .Index(t => t.TriggerTypeID);
            
            CreateTable(
                "dbo.TriggerTypeModels",
                c => new
                    {
                        TriggerTypeID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.TriggerTypeID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TriggerModels", "TriggerTypeID", "dbo.TriggerTypeModels");
            DropIndex("dbo.TriggerModels", new[] { "TriggerTypeID" });
            DropTable("dbo.TriggerTypeModels");
            DropTable("dbo.TriggerModels");
        }
    }
}
