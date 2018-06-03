using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvestmentPlatform.Domain.Models
{
    public class Solution
    {   
        public Solution()
        {
            Industries = new List<Industry>();
            SolutionTypes = new List<SolutionType>();
        }

        public int Id { get; set; }

        public string Title { get; set; }

        public string LogoFileName { get; set; }

        public string SolutionDescription { get; set; }

        public int InvestmentSize { get; set; }

        public string UniqueInfo { get; set; }

        public DateTime DateCreated { get; set; }

        [ForeignKey("Currency")]
        public int CurrencyId { get; set; }

        public Currency Currency { get; set; }

        [ForeignKey("ImplementationStatus")]
        public int ImplementationStatusId { get; set; }

        [ForeignKey("City")]
        public int CityId { get; set; }

        public City City { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public List<Industry> Industries { get; set; }

        public List<SolutionType> SolutionTypes { get; set; }

        public ImplementationStatus ImplementationStatus { get; set; }
    }
}
