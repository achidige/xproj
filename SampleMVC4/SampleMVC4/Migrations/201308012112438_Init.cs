namespace SampleMVC4.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Component",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Domain",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Study",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Component_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Component", t => t.Component_Id)
                .Index(t => t.Component_Id);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.Study", new[] { "Component_Id" });
            DropForeignKey("dbo.Study", "Component_Id", "dbo.Component");
            DropTable("dbo.Study");
            DropTable("dbo.Domain");
            DropTable("dbo.Component");
        }
    }
}
