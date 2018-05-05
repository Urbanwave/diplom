using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using InvestmentPlatform.Domain.Models;

namespace InvestmentPlatform.Models
{
    public class SolutionViewModel
    {
        [Required]
        [Display(Name = "*Solution title:")]
        public string Title { get; set; }

        [Required]
        public int CityId { get; set; }
        
        [Required]
        public int ImplementationStatusId { get; set; }

        [Required]
        [Display(Name = "*Solution description:")]
        public string SolutionDescription { get; set; }
        
        public List<SolutionType> SolutionTypes { get; set; }

        public List<int> SelectedSolutionTypes { get; set; }


        [Required]
        public int CurrencyId { get; set; }

        [Required]
        public int FromInvestmentSize { get; set; }

        [Required]
        public int ToInvestmentSize { get; set; }

        [Required]
        [Display(Name = "*Industry:")]
        public string Industry { get; set; }

        [Display(Name = "Something unique about solution:")]
        public string UniqueInfo { get; set; }
    }
}