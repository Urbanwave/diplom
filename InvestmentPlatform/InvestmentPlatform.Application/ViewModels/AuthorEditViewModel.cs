using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvestmentPlatform.Application.ViewModels
{
    public class AuthorEditViewModel
    {
        [Required(ErrorMessage = "First Name is required")]
        [Display(Name = "*First Name:")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        [Display(Name = "*Last Name:")]
        public string LastName { get; set; }

        [Display(Name = "*Email:")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Job Title is required")]
        [Display(Name = "*Job title:")]
        public string JobTitle { get; set; }

        [Display(Name = "Website:")]
        public string Website { get; set; }

        public string Id { get; set; }

        [Required]
        public int CityId { get; set; }

        public string FileName { get; set; }
    }
}
