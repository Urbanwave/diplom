namespace InvestmentPlatform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateSolution : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Solutions", "SolutionTypeId", "dbo.SolutionTypes");
            DropIndex("dbo.Solutions", new[] { "SolutionTypeId" });
            CreateTable(
                "dbo.SolutionTypeSolutions",
                c => new
                    {
                        SolutionType_Id = c.Int(nullable: false),
                        Solution_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.SolutionType_Id, t.Solution_Id })
                .ForeignKey("dbo.SolutionTypes", t => t.SolutionType_Id, cascadeDelete: true)
                .ForeignKey("dbo.Solutions", t => t.Solution_Id, cascadeDelete: true)
                .Index(t => t.SolutionType_Id)
                .Index(t => t.Solution_Id);
            
            AddColumn("dbo.Solutions", "FromInvestmentSize", c => c.Int(nullable: false));
            AddColumn("dbo.Solutions", "ToInvestmentSize", c => c.Int(nullable: false));
            DropColumn("dbo.Solutions", "InvestmentSize");
            DropColumn("dbo.Solutions", "SolutionTypeId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Solutions", "SolutionTypeId", c => c.Int(nullable: false));
            AddColumn("dbo.Solutions", "InvestmentSize", c => c.Int(nullable: false));
            DropForeignKey("dbo.SolutionTypeSolutions", "Solution_Id", "dbo.Solutions");
            DropForeignKey("dbo.SolutionTypeSolutions", "SolutionType_Id", "dbo.SolutionTypes");
            DropIndex("dbo.SolutionTypeSolutions", new[] { "Solution_Id" });
            DropIndex("dbo.SolutionTypeSolutions", new[] { "SolutionType_Id" });
            DropColumn("dbo.Solutions", "ToInvestmentSize");
            DropColumn("dbo.Solutions", "FromInvestmentSize");
            DropTable("dbo.SolutionTypeSolutions");
            CreateIndex("dbo.Solutions", "SolutionTypeId");
            AddForeignKey("dbo.Solutions", "SolutionTypeId", "dbo.SolutionTypes", "Id", cascadeDelete: true);
        }
    }
}
