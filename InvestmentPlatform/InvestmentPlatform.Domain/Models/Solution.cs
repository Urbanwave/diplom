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
        public int Id { get; set; }

        public string Title { get; set; }

        public string LogoFileName { get; set; }

        public string SolutionDescription { get; set; }

        public int InvestmentSize { get; set; }

        public string UniqueInfo { get; set; }

        [ForeignKey("SolutionType")]
        public int SolutionTypeId { get; set; }

        public SolutionType SolutionType { get; set; }

        [ForeignKey("Currency")]
        public int CurrencyId { get; set; }

        public Currency Currency { get; set; }

        [ForeignKey("ImplementationStatus")]
        public int ImplementationStatusId { get; set; }

        public ImplementationStatus ImplementationStatus { get; set; }

        [ForeignKey("City")]
        public int CityId { get; set; }

        public City City { get; set; }

        public List<Industry> Industries { get; set; }
    }
}
