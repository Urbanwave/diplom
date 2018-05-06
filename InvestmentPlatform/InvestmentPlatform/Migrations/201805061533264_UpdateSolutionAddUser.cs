namespace InvestmentPlatform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateSolutionAddUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Solutions", "UserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Solutions", "UserId");
            AddForeignKey("dbo.Solutions", "UserId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Solutions", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Solutions", new[] { "UserId" });
            DropColumn("dbo.Solutions", "UserId");
        }
    }
}
