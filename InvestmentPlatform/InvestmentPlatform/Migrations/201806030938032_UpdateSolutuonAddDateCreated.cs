namespace InvestmentPlatform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateSolutuonAddDateCreated : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Solutions", "DateCreated", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Solutions", "DateCreated");
        }
    }
}
