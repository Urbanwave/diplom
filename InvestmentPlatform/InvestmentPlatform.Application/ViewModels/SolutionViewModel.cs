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
        public int Id { get; set; }

        [Required]
        [Display(Name = "*Solution title:")]
        public string Title { get; set; }

        [Required]
        public int CityId { get; set; }

        public City City { get; set; }

        [Required]
        public int ImplementationStatusId { get; set; }

        [Required]
        [Display(Name = "*Solution description:")]
        public string SolutionDescription { get; set; }
        
        public List<SolutionType> SolutionTypes { get; set; }

        public List<int> SelectedSolutionTypes { get; set; }

        [Required]
        public int CurrencyId { get; set; }

        public Currency Currency { get; set; }

        public ImplementationStatus ImplementationStatus { get; set; }

        [Required]
        public int InvestmentSize { get; set; }

        public List<Industry> Industries { get; set; }

        public List<int> SelectedIndustries { get; set; }

        [Display(Name = "Something unique about solution:")]
        public string UniqueInfo { get; set; }

        public string FileName { get; set; }
    }
}