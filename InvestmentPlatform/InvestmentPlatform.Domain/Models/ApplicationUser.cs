using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace InvestmentPlatform.Domain.Models
{
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string LogoFileName { get; set; }

        public string JobTitle { get; set; }

        public string Website { get; set; }

        public string CompanyName { get; set; }

        public string CompanyDescription { get; set; }

        [ForeignKey("City")]
        public int CityId { get; set; }

        public City City { get; set; }

        public List<Industry> Industries { get; set; }

        public int InvestmentSize { get; set; }

        [ForeignKey("Currency")]
        public int? CurrencyId { get; set; }

        public Currency Currency { get; set; }
    }
}
