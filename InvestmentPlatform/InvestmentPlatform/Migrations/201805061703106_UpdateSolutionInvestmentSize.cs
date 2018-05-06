namespace InvestmentPlatform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateSolutionInvestmentSize : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Solutions", "InvestmentSize", c => c.Int(nullable: false));
            DropColumn("dbo.Solutions", "FromInvestmentSize");
            DropColumn("dbo.Solutions", "ToInvestmentSize");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Solutions", "ToInvestmentSize", c => c.Int(nullable: false));
            AddColumn("dbo.Solutions", "FromInvestmentSize", c => c.Int(nullable: false));
            DropColumn("dbo.Solutions", "InvestmentSize");
        }
    }
}
