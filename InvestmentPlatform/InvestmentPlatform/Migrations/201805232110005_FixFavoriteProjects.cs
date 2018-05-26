namespace InvestmentPlatform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixFavoriteProjects : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.IndustrySolutions", newName: "SolutionIndustries");
            DropPrimaryKey("dbo.SolutionIndustries");
            CreateTable(
                "dbo.FavoriteSolutions",
                c => new
                    {
                        FollowedUserId = c.String(nullable: false, maxLength: 128),
                        FollowedSolutionId = c.Int(nullable: false),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => new { t.FollowedUserId, t.FollowedSolutionId })
                .Index(t => t.ApplicationUser_Id);
            
            AddPrimaryKey("dbo.SolutionIndustries", new[] { "Solution_Id", "Industry_Id" });
        }
        
        public override void Down()
        {
            DropIndex("dbo.FavoriteSolutions", new[] { "ApplicationUser_Id" });
            DropPrimaryKey("dbo.SolutionIndustries");
            DropTable("dbo.FavoriteSolutions");
            AddPrimaryKey("dbo.SolutionIndustries", new[] { "Industry_Id", "Solution_Id" });
            RenameTable(name: "dbo.SolutionIndustries", newName: "IndustrySolutions");
        }
    }
}
