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

        [Required(ErrorMessage = "Solution title is required")]
        [MinLength(3, ErrorMessage = "The Solution title must be at least {1} characters long.")]
        [MaxLength(100, ErrorMessage = "The Solution title must be not more than {1} characters long.")]
        [Display(Name = "*Solution title:")]
        public string Title { get; set; }

        [Required]
        public int CityId { get; set; }

        public City City { get; set; }

        [Required]
        public int ImplementationStatusId { get; set; }

        [Required(ErrorMessage = "Solution description is required")]
        [MinLength(50, ErrorMessage = "The Solution description must be at least {1} characters long.")]
        [MaxLength(2000, ErrorMessage = "The Solution description must be not more than {1} characters long.")]
        [Display(Name = "*Solution description:")]
        public string SolutionDescription { get; set; }
        
        public List<SolutionType> SolutionTypes { get; set; }

        public List<int> SelectedSolutionTypes { get; set; }

        [Required]
        public int CurrencyId { get; set; }

        public Currency Currency { get; set; }

        public ImplementationStatus ImplementationStatus { get; set; }

        [Required(ErrorMessage = "Investmaent size is required")]
        [Range(1, 2000000000, ErrorMessage = "The Investment size must be betwwen {1} and {2}.")]
        public int InvestmentSize { get; set; }

        public List<Industry> Industries { get; set; }

        public List<int> SelectedIndustries { get; set; }

        [MaxLength(500, ErrorMessage = "The Something unique about solution must be not more than {1} characters long.")]
        [Display(Name = "Something unique about solution:")]
        public string UniqueInfo { get; set; }

        public string FileName { get; set; }
    }
}