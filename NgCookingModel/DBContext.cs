
using System.Data.Entity;

namespace NgCooking.Model
{
    public class DBContext : DbContext
    { 
        public DbSet<Recette> Recettes { get; set; }  
        public DbSet<Comment> Comments { get; set; }   
        public DbSet<Ingredient> Ingredients { get; set; }  
        public DbSet<Category> Categories { get; set; } 
        protected override void OnModelCreating(DbModelBuilder modelBuilder) 
        {
            base.OnModelCreating(modelBuilder); 
           
        }
    }
}
