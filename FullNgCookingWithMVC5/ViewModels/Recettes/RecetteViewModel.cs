using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using ViewModels.Ingredients;

namespace ViewModels.Recettes
{
    public class RecetteViewModel
    {
        [Required]
        [StringLength(30, ErrorMessage = "La chaîne {0} doit comporter au moins {2} caractères.", MinimumLength = 3)]
        [Display(Name = "Le nom de votre recette")]
        public string Name { get; set; }
        [Required]
        public bool IsAvailable { get; set; }
        [DataType(DataType.Upload)]
        [Display(Name = "Photo")]
        public Byte[] Picture { get; set; }
        [Required]
        [StringLength(30, ErrorMessage = "La chaîne {0} doit comporter au moins {2} caractères.", MinimumLength = 3)]
        [Display(Name = "Préparation")]
        public string Preparation { get; set; }
        [Required]
        [Display(Name = "Catégorie")]
        public string Category { get; set; }
        [Required]
        public List<IngredientViewModel> Ingredients { get; set; }  

    }
}