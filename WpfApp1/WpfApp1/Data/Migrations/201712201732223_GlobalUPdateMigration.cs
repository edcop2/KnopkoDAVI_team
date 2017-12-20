namespace WpfApp1.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class GlobalUPdateMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Equips",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Title = c.String(),
                        Price = c.Double(nullable: false),
                        Count = c.Int(nullable: false),
                        DayNorm = c.Double(nullable: false),
                        WorkTime = c.Double(nullable: false),
                        AmoKoef = c.Double(nullable: false),
                        ElectroPower = c.Double(nullable: false),
                        RepairNorm = c.Double(nullable: false),
                        ElectroTarif = c.Double(nullable: false),
                        EffectFond = c.Double(nullable: false),
                        LoadCoef = c.Double(nullable: false),
                        Total = c.Double(nullable: false),
                        TotalAmo = c.Double(nullable: false),
                        TotalElectro = c.Double(nullable: false),
                        TotalRepair = c.Double(nullable: false),
                        ProjectId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Projects", t => t.ProjectId, cascadeDelete: true)
                .Index(t => t.ProjectId);
            
            CreateTable(
                "dbo.Projects",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ProjetName = c.String(),
                        BuildRepairCost = c.Double(nullable: false),
                        TipicalPacketsCost = c.Double(nullable: false),
                        CommunicationLinesCost = c.Double(nullable: false),
                        InfoDbCost = c.Double(nullable: false),
                        SlaveTrainingCost = c.Double(nullable: false),
                        Time = c.Double(nullable: false),
                        Price = c.Double(nullable: false),
                        Multi = c.Int(nullable: false),
                        ElectroTarif = c.Double(nullable: false),
                        AddPayment = c.Double(nullable: false),
                        SocPayment = c.Double(nullable: false),
                        DevCost = c.Double(nullable: false),
                        ImpCost = c.Double(nullable: false),
                        CapCost = c.Double(nullable: false),
                        ExploitCost = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Materials",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Title = c.String(),
                        Measure = c.String(),
                        Count = c.Double(nullable: false),
                        Price = c.Double(nullable: false),
                        ProjectId = c.Guid(nullable: false),
                        Total = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Projects", t => t.ProjectId, cascadeDelete: true)
                .Index(t => t.ProjectId);
            
            CreateTable(
                "dbo.Slaves",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Position = c.String(),
                        Salary = c.Double(nullable: false),
                        WorkDays = c.Int(nullable: false),
                        ExploitDays = c.Int(nullable: false),
                        ProjectId = c.Guid(nullable: false),
                        OneDaySalary = c.Double(nullable: false),
                        TotalPayment = c.Double(nullable: false),
                        ExploitPayment = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Projects", t => t.ProjectId, cascadeDelete: true)
                .Index(t => t.ProjectId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Slaves", "ProjectId", "dbo.Projects");
            DropForeignKey("dbo.Materials", "ProjectId", "dbo.Projects");
            DropForeignKey("dbo.Equips", "ProjectId", "dbo.Projects");
            DropIndex("dbo.Slaves", new[] { "ProjectId" });
            DropIndex("dbo.Materials", new[] { "ProjectId" });
            DropIndex("dbo.Equips", new[] { "ProjectId" });
            DropTable("dbo.Slaves");
            DropTable("dbo.Materials");
            DropTable("dbo.Projects");
            DropTable("dbo.Equips");
        }
    }
}
