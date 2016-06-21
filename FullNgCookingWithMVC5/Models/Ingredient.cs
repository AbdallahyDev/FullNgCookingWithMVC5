using System;
using System.Collections.Generic;

namespace NgCooking.Model
{
    public class Ingredient
    {
        public Ingredient()
        {
            Recettes = new HashSet<Recette>();  
        }
        public int Id { get; set; } 
        public string Name { get; set; }
        public bool IsAvailable { get; set; }    
        public virtual ICollection<Recette> Recettes { get; set; } 
        public Byte[] Picture { get; set; }
        public float Calories { get; set; }
        public string Category { get; set; }  
    }
}
