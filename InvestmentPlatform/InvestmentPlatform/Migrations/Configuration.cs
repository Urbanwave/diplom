namespace InvestmentPlatform.Migrations
{
    using InvestmentPlatform.Domain.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            //var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));

            //var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            //var role1 = new IdentityRole { Name = "Admin" };
            //var role2 = new IdentityRole { Name = "Author" };
            //var role3 = new IdentityRole { Name = "Investor" };

            //roleManager.Create(role1);
            //roleManager.Create(role2);
            //roleManager.Create(role3);

            //var admin = new ApplicationUser { Email = "Admin@yopmail.com", UserName = "Admin@yopmail.com", CityId = 13 };
            //string password = "123456";
            //var result = userManager.Create(admin, password);

            //if (result.Succeeded)
            //{
            //    userManager.AddToRole(admin.Id, role1.Name);
            //}
        }
    }
}
