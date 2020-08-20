using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ArtGalleryProject.Models
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

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Kodi")]
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
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Fjalekalimi")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Duhet te shkruani email-in.")]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Duhet te shkruani fjalekalimin.")]
        [StringLength(100, ErrorMessage = "{0} duhet te jete te pakten {2} karaktere i gjate.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Fjalekalimi")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Konfirmo fjalekalimin")]
        [Compare("Password", ErrorMessage = "Konfirmimi duhet te perputhet me fjalekalimin.")]
        public string ConfirmPassword { get; set; }
      
        [Required (ErrorMessage ="Duhet te plotesoni fushen Rreth jush.")]
        [Display(Name ="Rreth jush")]
        public string bio { get; set; }

        [Required(ErrorMessage = "Duhet te shkruani emrin.")]
        [RegularExpression("^[A-Za-z]+$", ErrorMessage = "Emri duhet te permbaje vetem shkronja.")]
        public string Emri { get; set; }

        [Required(ErrorMessage = "Duhet te shkruani mbiemrin.")]
        [RegularExpression("^[A-Za-z]+$", ErrorMessage = "Mbiemri duhet te permbaje vetem shkronja.")]
        public string Mbiemri { get; set; }
  }

    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "{0} duhet te jete {2} karaktere i gjate.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Fjalekalimi")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Konfirmo fjalekalimin")]
        [Compare("Password", ErrorMessage = "Konfirmimi nuk perputhet me fjalekalimin.")]
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
