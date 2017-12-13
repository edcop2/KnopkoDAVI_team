namespace WpfApp1.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProjectChangedMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CbQjs",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        C = c.Double(nullable: false),
                        Q = c.Double(nullable: false),
                        DevCostId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DevCosts", t => t.DevCostId, cascadeDelete: true)
                .Index(t => t.DevCostId);
            
            CreateTable(
                "dbo.DevCosts",
                c => new
                    {
                        ProjectId = c.Guid(nullable: false),
                        ChiefCost = c.Double(nullable: false),
                        SlaveCost = c.Double(nullable: false),
                        MaterialCost = c.Double(nullable: false),
                        DevTime = c.Double(nullable: false),
                        EqCount = c.Int(nullable: false),
                        RoomRecCost = c.Double(nullable: false),
                        PacketCost = c.Double(nullable: false),
                        ComLinesCost = c.Double(nullable: false),
                        InfoDbCost = c.Double(nullable: false),
                        SlaveTrainingCost = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.ProjectId)
                .ForeignKey("dbo.Projects", t => t.ProjectId, cascadeDelete: true)
                .Index(t => t.ProjectId);
            
            CreateTable(
                "dbo.Projects",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ProjetName = c.String(),
                        Competitivness = c.Double(),
                        DevCosts = c.Double(),
                        OpCosts = c.Double(),
                        EcoEffect = c.Double(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ExpertEval",
                c => new
                    {
                        ProjectId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.ProjectId)
                .ForeignKey("dbo.Projects", t => t.ProjectId, cascadeDelete: true)
                .Index(t => t.ProjectId);
            
            CreateTable(
                "dbo.Eval",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Val = c.Int(nullable: false),
                        ExpertEvalId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ExpertEval", t => t.ExpertEvalId, cascadeDelete: true)
                .Index(t => t.ExpertEvalId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CbQjs", "DevCostId", "dbo.DevCosts");
            DropForeignKey("dbo.DevCosts", "ProjectId", "dbo.Projects");
            DropForeignKey("dbo.ExpertEval", "ProjectId", "dbo.Projects");
            DropForeignKey("dbo.Eval", "ExpertEvalId", "dbo.ExpertEval");
            DropIndex("dbo.Eval", new[] { "ExpertEvalId" });
            DropIndex("dbo.ExpertEval", new[] { "ProjectId" });
            DropIndex("dbo.DevCosts", new[] { "ProjectId" });
            DropIndex("dbo.CbQjs", new[] { "DevCostId" });
            DropTable("dbo.Eval");
            DropTable("dbo.ExpertEval");
            DropTable("dbo.Projects");
            DropTable("dbo.DevCosts");
            DropTable("dbo.CbQjs");
        }
    }
}
