namespace WpfApp1.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OpCostsAddedMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EmpTimes",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        TimeT = c.Double(nullable: false),
                        TimeZ = c.Double(nullable: false),
                        OpCostId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.OpCosts", t => t.OpCostId, cascadeDelete: true)
                .Index(t => t.OpCostId);
            
            CreateTable(
                "dbo.OpCosts",
                c => new
                    {
                        ProjectId = c.Guid(nullable: false),
                        EmpCount = c.Int(nullable: false),
                        EqCount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ProjectId)
                .ForeignKey("dbo.Projects", t => t.ProjectId, cascadeDelete: true)
                .Index(t => t.ProjectId);
            
            CreateTable(
                "dbo.Equips",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        YearNorm = c.Double(nullable: false),
                        Price = c.Double(nullable: false),
                        Count = c.Double(nullable: false),
                        WorkTime = c.Double(nullable: false),
                        Power = c.Double(nullable: false),
                        OpCostId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.OpCosts", t => t.OpCostId, cascadeDelete: true)
                .Index(t => t.OpCostId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EmpTimes", "OpCostId", "dbo.OpCosts");
            DropForeignKey("dbo.OpCosts", "ProjectId", "dbo.Projects");
            DropForeignKey("dbo.Equips", "OpCostId", "dbo.OpCosts");
            DropIndex("dbo.Equips", new[] { "OpCostId" });
            DropIndex("dbo.OpCosts", new[] { "ProjectId" });
            DropIndex("dbo.EmpTimes", new[] { "OpCostId" });
            DropTable("dbo.Equips");
            DropTable("dbo.OpCosts");
            DropTable("dbo.EmpTimes");
        }
    }
}
