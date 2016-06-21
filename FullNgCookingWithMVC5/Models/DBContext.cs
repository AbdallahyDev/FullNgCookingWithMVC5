using Microsoft.AspNet.Identity.EntityFramework;
using NgCooking.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace FullNgCookingWithMVC5.Models
{
    public class ApplicationDbContext : IdentityDbContext<NgCookingUser>
    {
        public DbSet<Recette> Recettes { get; set; }
        public DbSet<Comment> Comments { get; set; }  
        public DbSet<Ingredient> Ingredients { get; set; } 
        public DbSet<Category> Categories { get; set; }  
        public ApplicationDbContext()
            : base("NgCookingDB", throwIfV1Schema: false)  
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();     
        }
    }  
}
    
