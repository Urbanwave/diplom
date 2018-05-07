namespace InvestmentPlatform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateAppUserInvestor : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "InvestmentSize", c => c.Int(nullable: false));
            AddColumn("dbo.AspNetUsers", "CurrencyId", c => c.Int());
            CreateIndex("dbo.AspNetUsers", "CurrencyId");
            AddForeignKey("dbo.AspNetUsers", "CurrencyId", "dbo.Currencies", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "CurrencyId", "dbo.Currencies");
            DropIndex("dbo.AspNetUsers", new[] { "CurrencyId" });
            DropColumn("dbo.AspNetUsers", "CurrencyId");
            DropColumn("dbo.AspNetUsers", "InvestmentSize");
        }
    }
}
