using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ViewModels.Ingredients;

namespace ViewModels.Recettes
{
    public class RecetteViewModel
    {
        public string Name { get; set; }
        public bool IsAvailable { get; set; }
        public Byte[] Picture { get; set; } 
        public string Preparation { get; set; }
        public string Category { get; set; }
        public List<IngredientViewModel> Ingredients { get; set; }  

    }
}