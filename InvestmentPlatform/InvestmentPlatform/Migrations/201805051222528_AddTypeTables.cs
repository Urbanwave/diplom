namespace InvestmentPlatform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTypeTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Industries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Solutions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        LogoFileName = c.String(),
                        SolutionDescription = c.String(),
                        InvestmentSize = c.Int(nullable: false),
                        UniqueInfo = c.String(),
                        SolutionTypeId = c.Int(nullable: false),
                        CurrencyId = c.Int(nullable: false),
                        ImplementationStatusId = c.Int(nullable: false),
                        CityId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cities", t => t.CityId, cascadeDelete: true)
                .ForeignKey("dbo.Currencies", t => t.CurrencyId, cascadeDelete: true)
                .ForeignKey("dbo.ImplementationStatus", t => t.ImplementationStatusId, cascadeDelete: true)
                .ForeignKey("dbo.SolutionTypes", t => t.SolutionTypeId, cascadeDelete: true)
                .Index(t => t.SolutionTypeId)
                .Index(t => t.CurrencyId)
                .Index(t => t.ImplementationStatusId)
                .Index(t => t.CityId);
            
            CreateTable(
                "dbo.Currencies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ImplementationStatus",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SolutionTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.IndustryApplicationUsers",
                c => new
                    {
                        Industry_Id = c.Int(nullable: false),
                        ApplicationUser_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.Industry_Id, t.ApplicationUser_Id })
                .ForeignKey("dbo.Industries", t => t.Industry_Id, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id, cascadeDelete: true)
                .Index(t => t.Industry_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.SolutionIndustries",
                c => new
                    {
                        Solution_Id = c.Int(nullable: false),
                        Industry_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Solution_Id, t.Industry_Id })
                .ForeignKey("dbo.Solutions", t => t.Solution_Id, cascadeDelete: true)
                .ForeignKey("dbo.Industries", t => t.Industry_Id, cascadeDelete: true)
                .Index(t => t.Solution_Id)
                .Index(t => t.Industry_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Solutions", "SolutionTypeId", "dbo.SolutionTypes");
            DropForeignKey("dbo.SolutionIndustries", "Industry_Id", "dbo.Industries");
            DropForeignKey("dbo.SolutionIndustries", "Solution_Id", "dbo.Solutions");
            DropForeignKey("dbo.Solutions", "ImplementationStatusId", "dbo.ImplementationStatus");
            DropForeignKey("dbo.Solutions", "CurrencyId", "dbo.Currencies");
            DropForeignKey("dbo.Solutions", "CityId", "dbo.Cities");
            DropForeignKey("dbo.IndustryApplicationUsers", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.IndustryApplicationUsers", "Industry_Id", "dbo.Industries");
            DropIndex("dbo.SolutionIndustries", new[] { "Industry_Id" });
            DropIndex("dbo.SolutionIndustries", new[] { "Solution_Id" });
            DropIndex("dbo.IndustryApplicationUsers", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.IndustryApplicationUsers", new[] { "Industry_Id" });
            DropIndex("dbo.Solutions", new[] { "CityId" });
            DropIndex("dbo.Solutions", new[] { "ImplementationStatusId" });
            DropIndex("dbo.Solutions", new[] { "CurrencyId" });
            DropIndex("dbo.Solutions", new[] { "SolutionTypeId" });
            DropTable("dbo.SolutionIndustries");
            DropTable("dbo.IndustryApplicationUsers");
            DropTable("dbo.SolutionTypes");
            DropTable("dbo.ImplementationStatus");
            DropTable("dbo.Currencies");
            DropTable("dbo.Solutions");
            DropTable("dbo.Industries");
        }
    }
}
