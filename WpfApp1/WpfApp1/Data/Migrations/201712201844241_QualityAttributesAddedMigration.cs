namespace WpfApp1.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class QualityAttributesAddedMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.QualityAttributes",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Title = c.String(),
                        Coef = c.Double(nullable: false),
                        MyValue = c.Int(nullable: false),
                        MyQu = c.Double(nullable: false),
                        ProjectId = c.Guid(nullable: false),
                        OtherValue = c.Int(),
                        OtherQu = c.Double(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Projects", t => t.ProjectId, cascadeDelete: true)
                .Index(t => t.ProjectId);
            
            AddColumn("dbo.Projects", "ProjectTechLevel", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.QualityAttributes", "ProjectId", "dbo.Projects");
            DropIndex("dbo.QualityAttributes", new[] { "ProjectId" });
            DropColumn("dbo.Projects", "ProjectTechLevel");
            DropTable("dbo.QualityAttributes");
        }
    }
}
