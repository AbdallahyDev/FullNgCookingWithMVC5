using Microsoft.AspNet.Identity.EntityFramework;
using Model.Comments;
using Models.Communities;
using Models.Ctegories;
using Models.Ingredients;
using Models.Recettes;
using System.Data.Entity;

namespace FullNgCookingWithMVC5.Models
{
    public class NgCookingDbContext : IdentityDbContext<NgCookingUser>
    {
        public DbSet<Recette> Recettes { get; set; }
        public DbSet<Comment> Comments { get; set; }  
        public DbSet<Ingredient> Ingredients { get; set; } 
        public DbSet<Category> Categories { get; set; }  
        public NgCookingDbContext()
            : base("NgCookingDB", throwIfV1Schema: false)   
        {
        }

        public static NgCookingDbContext Create() 
        {
            return new NgCookingDbContext();      
        }

        //public System.Data.Entity.DbSet<Models.Users.NgCookingUser> NgCookingUsers { get; set; }
    }  
}
    
