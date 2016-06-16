using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FullNgCookingWithMVC5.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Courrier électronique")]
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
        [Display(Name = "Code")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Mémoriser ce navigateur ?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Courrier électronique")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Courrier électronique")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Mot de passe")]
        public string Password { get; set; }

        [Display(Name = "Mémoriser le mot de passe ?")] 
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required]
        [StringLength(30, ErrorMessage = "La chaîne {0} doit comporter au moins {2} caractères.", MinimumLength = 3)]
        [Display(Name = "Nom")]
        public string FirstName { get; set; }
        [Required]
        [StringLength(30, ErrorMessage = "La chaîne {0} doit comporter au moins {2} caractères.", MinimumLength = 3)]
        [Display(Name = "Prénom")]
        public string Surname { get; set; } 

        [Required]
        [StringLength(20, ErrorMessage = "La chaîne {0} doit comporter au moins {2} caractères.", MinimumLength = 3)]
        [Display(Name = "Ville")]
        public string City { get; set; }

        [Required] 
        [StringLength(2048, ErrorMessage = "La chaîne {0} doit comporter au moins {2} caractères.", MinimumLength = 10)]
        [Display(Name = "Bio")]  
        public string Bio { get; set; }
        [Display(Name = "Date de naissance")]
        [Required]
        [DataType(DataType.Date)] 
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]  
        public DateTime BirthDate { get; set; }

        [Required]
        [Range(1, 3, ErrorMessage = "{0} doit être entre {2} et {1}")]  
        [Display(Name = "Niveau")]
        public int Level { get; set; }

        [DataType(DataType.Upload)] 
        [Display(Name = "Photo")] 
        public byte[] Picture { get; set; }    

        [Required]
        [EmailAddress]
        [Display(Name = "Courrier électronique")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "La chaîne {0} doit comporter au moins {2} caractères.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Mot de passe")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmer le mot de passe ")]
        [Compare("Password", ErrorMessage = "Le mot de passe et le mot de passe de confirmation ne correspondent pas.")]
        public string ConfirmPassword { get; set; }
    }

    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Courrier électronique")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "La chaîne {0} doit comporter au moins {2} caractères.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Mot de passe")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmer le mot de passe")]
        [Compare("Password", ErrorMessage = "Le nouveau mot de passe et le mot de passe de confirmation ne correspondent pas.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "E-mail")]
        public string Email { get; set; }
    }
}
