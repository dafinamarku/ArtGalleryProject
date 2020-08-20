using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using ArtGalleryProject.Models;

namespace ArtGalleryProject.Models
{
    public class IndexViewModel
  {
    /// <summary>
    /// ////////////////////////////////////////////////////////////////////
    /// </summary>
       public ApplicationUser user { get; set; }
        public IEnumerable<Artwork> veprat { get; set; }
        public int nrfollowers { get; set; }
        public int nrfollowing { get; set; }

        public bool HasPassword { get; set; }
        public IList<UserLoginInfo> Logins { get; set; }
        public string PhoneNumber { get; set; }
        public bool TwoFactor { get; set; }
        public bool BrowserRemembered { get; set; }
    }

    public class ManageLoginsViewModel
    {
        public IList<UserLoginInfo> CurrentLogins { get; set; }
        public IList<AuthenticationDescription> OtherLogins { get; set; }
    }

    public class FactorViewModel
    {
        public string Purpose { get; set; }
    }

    public class SetPasswordViewModel
    {
        [Required]
        [StringLength(100, ErrorMessage = "{0} duhet te jete {2} karaktere i gjate.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Fjalekalimi i ri")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Konfirmoni fjalekalimin e ri")]
        [Compare("NewPassword", ErrorMessage = "Fjalekalimi i ri dhe konfirmimi nuk perputhen")]
        public string ConfirmPassword { get; set; }
    }

    public class ChangePasswordViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Fjalekalimi aktual")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "{0} duhet te jete {2} karaktere i gjate.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Fjalekalimi i ri")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Konfirmoni fjalekalimin e ri")]
        [Compare("NewPassword", ErrorMessage = "Fjalekalimi i ri dhe konfirmimi nuk perputhen.")]
        public string ConfirmPassword { get; set; }
    }

    public class AddPhoneNumberViewModel
    {
        [Required]
        [Phone]
        [Display(Name = "Nr telefonit")]
        public string Number { get; set; }
    }

    public class VerifyPhoneNumberViewModel
    {
        [Required]
        [Display(Name = "Kodi")]
        public string Code { get; set; }

        [Required]
        [Phone]
        [Display(Name = "Nr telefonit")]
        public string PhoneNumber { get; set; }
    }

    public class ConfigureTwoFactorViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
    }
}