namespace WpfApp1.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class ExpertEvalMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Eval",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Val = c.Int(nullable: false),
                    ExpertEvalId = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ExpertEval", t => t.ExpertEvalId, cascadeDelete: true)
                .Index(t => t.ExpertEvalId);

            CreateTable(
                "dbo.ExpertEval",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    ProjetName = c.String(),
                })
                .PrimaryKey(t => t.Id);

        }

        public override void Down()
        {
            DropForeignKey("dbo.Eval", "ExpertEvalId", "dbo.ExpertEval");
            DropIndex("dbo.Eval", new[] { "ExpertEvalId" });
            DropTable("dbo.ExpertEval");
            DropTable("dbo.Eval");
        }
    }
}
