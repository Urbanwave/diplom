using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace InvestmentPlatform.Models
{
    public class EditSolutionViewModel
    {
        [Required]
        [Display(Name = "*Solution title:")]
        public string Title { get; set; }

        [Required]
        [Display(Name = "*Location:")]
        public string Location { get; set; }

        [Required]
        [Display(Name = "*Solution description:")]
        public string SolutionDescription { get; set; }
        
        [Display(Name = "Product")]
        public bool KindProduct { get; set; }

        [Display(Name = "Project")]
        public bool KindProject { get; set; }

        [Display(Name = "Service")]
        public bool KindService { get; set; }

        [Required]
        [Display(Name = "*Industry:")]
        public string Industry { get; set; }

        [Display(Name = "Images:")]
        public string Images { get; set; }

        [Required]
        [Display(Name = "*Project implementation status:")]
        public string ImplementationStatus { get; set; }

        [Required]
        [Display(Name = "*The total cost of the project:")]
        public string TotalCost { get; set; }

        [Required]
        [Display(Name = "*Investment size:")]
        public string InvestmentSize { get; set; }

        [Display(Name = "Something unique about solution:")]
        public string UniqueInfo { get; set; }
    }
}