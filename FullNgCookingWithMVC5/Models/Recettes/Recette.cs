using Model.Comments;
using Models.Ingredients;
using System;
using System.Collections.Generic;

namespace Models.Recettes
{
    public class Recette
    {
        public Recette()
        {
            Ingredients = new HashSet<Ingredient>();    
        }
        public string Id { get; set; }  
        public ICollection<Ingredient> Ingredients { get; set; }
        public string Name { get; set; }
        public bool IsAvailable { get; set; }
        public Byte[] Picture { get; set; } 
        public float Calories { get; set; }
        public string Preparation { get; set; }
        public DateTime CreationDate { get; set; }  
        public virtual List<Comment> Comments { get; set; }
        public string Category { get; set; }      
        public string CreatorId { get; set; }     
    }
}
