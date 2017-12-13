namespace WpfApp1.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EcoEffectAddedMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EcoEffects",
                c => new
                    {
                        ProjectId = c.Guid(nullable: false),
                        NetCost1 = c.Double(nullable: false),
                        NetCost2 = c.Double(nullable: false),
                        ImplementCost1 = c.Double(nullable: false),
                        ImplementCost2 = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.ProjectId)
                .ForeignKey("dbo.Projects", t => t.ProjectId, cascadeDelete: true)
                .Index(t => t.ProjectId);
            
            AddColumn("dbo.Projects", "EcoEffects", c => c.Double());
            DropColumn("dbo.Projects", "EcoEffect");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Projects", "EcoEffect", c => c.Double());
            DropForeignKey("dbo.EcoEffects", "ProjectId", "dbo.Projects");
            DropIndex("dbo.EcoEffects", new[] { "ProjectId" });
            DropColumn("dbo.Projects", "EcoEffects");
            DropTable("dbo.EcoEffects");
        }
    }
}
