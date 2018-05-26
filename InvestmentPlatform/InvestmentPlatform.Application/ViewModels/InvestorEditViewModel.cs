using InvestmentPlatform.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvestmentPlatform.Application.ViewModels
{
    public class InvestorEditViewModel
    {
        [Required(ErrorMessage = "First Name is required")]
        [Display(Name = "*First Name:")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        [Display(Name = "*Last Name:")]
        public string LastName { get; set; }

        [Display(Name = "*Email:")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Company name is required")]
        [Display(Name = "*Company name:")]
        public string CompanyName { get; set; }

        [Required(ErrorMessage = "Company description is required")]
        [Display(Name = "*Company description:")]
        public string CompanyDescription { get; set; }

        [Display(Name = "Website:")]
        public string Website { get; set; }

        public string Id { get; set; }

        public City City { get; set; }

        public Currency Curency { get; set; }

        public string FileName { get; set; }

        [Required]
        public int CityId { get; set; }

        [Required]
        public int CurrencyId { get; set; }

        public List<Industry> InvestmentSectors { get; set; }

        public List<int> SelectedInvestmentSectors { get; set; }

        [Required(ErrorMessage = "Investment size is required")]
        [Display(Name = "*Investment size:")]
        public int InvestmentSize { get; set; }
    }
}
