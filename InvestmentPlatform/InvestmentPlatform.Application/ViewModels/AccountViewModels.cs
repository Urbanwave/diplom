using InvestmentPlatform.Domain.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InvestmentPlatform.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    //public class SendCodeViewModel
    //{
    //    public string SelectedProvider { get; set; }
    //    public ICollection<SelectListItem> Providers { get; set; }
    //    public string ReturnUrl { get; set; }
    //    public bool RememberMe { get; set; }
    //}

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Remember this browser?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Email:")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password:")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    public class InvestorRegisterViewModel
    {
        [Required(ErrorMessage = "First Name is required")]
        [Display(Name = "*First Name:")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        [Display(Name = "*Last Name:")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        [Display(Name = "*Email:")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "*Password:")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "*Confirm password:")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

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

    public class AuthorRegisterViewModel
    {
        [Required(ErrorMessage = "First Name is required")]
        [Display(Name = "*First Name:")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        [Display(Name = "*Last Name:")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        [Display(Name = "*Email:")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "*Password:")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "*Confirm password:")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Job Title is required")]
        [Display(Name = "*Job title:")]
        public string JobTitle { get; set; }

        [Display(Name = "Website:")]
        public string Website { get; set; }

        [Required]
        public int CityId { get; set; }   
    }

    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "*Email:")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "*Password:")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "*Confirm password:")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

    }
        public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
