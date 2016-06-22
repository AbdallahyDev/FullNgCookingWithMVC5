using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace WebAPI.Models
{
    // Modèles utilisés comme paramètres des actions AccountController.

    public class AddExternalLoginBindingModel
    {
        [Required]
        [Display(Name = "Jeton d’accès externe")]
        public string ExternalAccessToken { get; set; }
    }

    public class ChangePasswordBindingModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Mot de passe actuel")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "La chaîne {0} doit comporter au moins {2} caractères.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Nouveau mot de passe")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmer le nouveau mot de passe")]
        [Compare("NewPassword", ErrorMessage = "Le nouveau mot de passe et le mot de passe de confirmation ne correspondent pas.")]
        public string ConfirmPassword { get; set; }
    }

    public class RegisterBindingModel 
    {
        #region The new model
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
        [Display(Name = "Naissance")]
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Birth { get; set; }

        [Required]
        [Range(1, 3, ErrorMessage = "{0} doit être entre {2} et {1}")]
        [Display(Name = "Niveau")]
        public int Level { get; set; }

        [DataType(DataType.Upload)]
        [Display(Name = "Photo")]
        public byte[] Picture { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
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
        #endregion
        //[Required]
        //[Display(Name = "E-mail")]
        //public string Email { get; set; }

        //[Required]
        //[StringLength(100, ErrorMessage = "La chaîne {0} doit comporter au moins {2} caractères.", MinimumLength = 6)]
        //[DataType(DataType.Password)]
        //[Display(Name = "Mot de passe")]
        //public string Password { get; set; }

        //[DataType(DataType.Password)]
        //[Display(Name = "Confirmer le mot de passe ")]
        //[Compare("Password", ErrorMessage = "Le mot de passe et le mot de passe de confirmation ne correspondent pas.")]
        //public string ConfirmPassword { get; set; }
    }

    public class RegisterExternalBindingModel
    {
        [Required]
        [Display(Name = "E-mail")]
        public string Email { get; set; }
    }

    public class RemoveLoginBindingModel
    {
        [Required]
        [Display(Name = "Fournisseur de connexion")]
        public string LoginProvider { get; set; }

        [Required]
        [Display(Name = "Clé du fournisseur")]
        public string ProviderKey { get; set; }
    }

    public class SetPasswordBindingModel
    {
        [Required]
        [StringLength(100, ErrorMessage = "La chaîne {0} doit comporter au moins {2} caractères.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Nouveau mot de passe")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmer le nouveau mot de passe")]
        [Compare("NewPassword", ErrorMessage = "Le nouveau mot de passe et le mot de passe de confirmation ne correspondent pas.")]
        public string ConfirmPassword { get; set; }
    }
}
