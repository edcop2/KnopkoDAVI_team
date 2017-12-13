namespace WpfApp1.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DbWipeMigration : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Eval", "ExpertEvalId", "dbo.ExpertEval");
            DropIndex("dbo.Eval", new[] { "ExpertEvalId" });
            DropTable("dbo.Eval");
            DropTable("dbo.ExpertEval");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ExpertEval",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ProjetName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Eval",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Val = c.Int(nullable: false),
                        ExpertEvalId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.Eval", "ExpertEvalId");
            AddForeignKey("dbo.Eval", "ExpertEvalId", "dbo.ExpertEval", "Id", cascadeDelete: true);
        }
    }
}
