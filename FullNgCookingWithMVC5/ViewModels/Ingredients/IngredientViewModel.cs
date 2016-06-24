using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ViewModels.Ingredients
{
    public class IngredientViewModel
    {
        [Required]
        [StringLength(30, ErrorMessage = "La chaîne {0} doit comporter au moins {2} caractères.", MinimumLength = 3)]
        [Display(Name = "Nom")]
        public string Name { get; set; }
        public bool IsAvailable { get; set; } 
        [DataType(DataType.Upload)]
        [Display(Name = "Image")] 
        public byte[] Picture { get; set; }
        [Required]
        [Range(1, 1000, ErrorMessage = "{0} doit être entre {2} et {1}")] 
        [Display(Name = "Calorie")]
        public float Calories { get; set; } 
        //[Required]
        //[StringLength(30, ErrorMessage = "La chaîne {0} doit comporter au moins {2} caractères.", MinimumLength = 3)]
        //[Display(Name = "Categorie")]
       
        public string Category { get; set; }
    }
}