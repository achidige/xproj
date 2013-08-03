namespace SampleMVC4.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class domainvar : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Variable", "Domain_Id", c => c.Int());
            AddForeignKey("dbo.Variable", "Domain_Id", "dbo.Domain", "Id");
            CreateIndex("dbo.Variable", "Domain_Id");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Variable", new[] { "Domain_Id" });
            DropForeignKey("dbo.Variable", "Domain_Id", "dbo.Domain");
            DropColumn("dbo.Variable", "Domain_Id");
        }
    }
}
