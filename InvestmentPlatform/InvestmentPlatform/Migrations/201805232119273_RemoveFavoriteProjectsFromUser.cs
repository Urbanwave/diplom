namespace InvestmentPlatform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveFavoriteProjectsFromUser : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.FavoriteSolutions", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.FavoriteSolutions", new[] { "ApplicationUser_Id" });
            DropColumn("dbo.FavoriteSolutions", "ApplicationUser_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.FavoriteSolutions", "ApplicationUser_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.FavoriteSolutions", "ApplicationUser_Id");
            AddForeignKey("dbo.FavoriteSolutions", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
