namespace InvestmentPlatform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateApplicationUserAddFavoriteProjects : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.SolutionIndustries", newName: "IndustrySolutions");
            DropPrimaryKey("dbo.IndustrySolutions");
            AddPrimaryKey("dbo.IndustrySolutions", new[] { "Industry_Id", "Solution_Id" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.IndustrySolutions");
            AddPrimaryKey("dbo.IndustrySolutions", new[] { "Solution_Id", "Industry_Id" });
            RenameTable(name: "dbo.IndustrySolutions", newName: "SolutionIndustries");
        }
    }
}
