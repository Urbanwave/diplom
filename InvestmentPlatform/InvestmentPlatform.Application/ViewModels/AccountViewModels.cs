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
        [Required(ErrorMessage = "Email is required")]
        [Display(Name = "Email:")]
        [MaxLength(50, ErrorMessage = "The Email must be not more than {1} characters long.")]
        [EmailAddress(ErrorMessage = "Email is not valid.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [Display(Name = "Password:")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    public class InvestorRegisterViewModel
    {
        [Required(ErrorMessage = "First Name is required")]
        [MaxLength(45, ErrorMessage = "The First Name must be not more than {1} characters long.")]
        [Display(Name = "*First Name:")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        [MaxLength(45, ErrorMessage = "The Last Name must be not more than {1} characters long.")]
        [Display(Name = "*Last Name:")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [MaxLength(50, ErrorMessage = "The Email must be not more than {1} characters long.")]
        [EmailAddress(ErrorMessage = "Email is not valid.")]
        [Display(Name = "*Email:")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [MinLength(6, ErrorMessage = "The Password must be at least {1} characters long.")]
        [MaxLength(30, ErrorMessage = "The Password must be not more than {1} characters long.")]
        [DataType(DataType.Password)]
        [Display(Name = "*Password:")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "*Confirm password:")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Company name is required")]
        [MaxLength(100, ErrorMessage = "The Company name must be not more than {1} characters long.")]
        [Display(Name = "*Company name:")]
        public string CompanyName { get; set; }

        [Required(ErrorMessage = "Company description is required")]
        [MinLength(50, ErrorMessage = "The Company description must be at least {1} characters long.")]
        [MaxLength(2000, ErrorMessage = "The Company description must be not more than {1} characters long.")]
        [Display(Name = "*Company description:")]
        public string CompanyDescription { get; set; }

        [MaxLength(100, ErrorMessage = "The Website must be not more than {1} characters long.")]
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
        [Range(1, 2000000000, ErrorMessage = "The Investment size must be betwwen {1} and {2}.")]
        [Display(Name = "*Investment size:")]
        public int InvestmentSize { get; set; }
    }

    public class AuthorRegisterViewModel
    {
        [Required(ErrorMessage = "First Name is required")]
        [MaxLength(45, ErrorMessage = "The First Name must be not more than {1} characters long.")]
        [Display(Name = "*First Name:")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        [MaxLength(45, ErrorMessage = "The Last Name must be not more than {1} characters long.")]
        [Display(Name = "*Last Name:")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [MaxLength(50, ErrorMessage = "The Email must be not more than {1} characters long.")]
        [EmailAddress(ErrorMessage = "Email is not valid.")]
        [Display(Name = "*Email:")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [MinLength(6, ErrorMessage = "The Password must be at least {1} characters long.")]
        [MaxLength(30, ErrorMessage = "The Password must be not more than {1} characters long.")]
        [DataType(DataType.Password)]
        [Display(Name = "*Password:")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "*Confirm password:")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Job title is required")]
        [MaxLength(100, ErrorMessage = "The Job title must be not more than {1} characters long.")]
        [Display(Name = "*Job title:")]
        public string JobTitle { get; set; }

        //[Url(ErrorMessage = "Website is not valid")]
        [MaxLength(100, ErrorMessage = "The Website must be not more than {1} characters long.")]
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
