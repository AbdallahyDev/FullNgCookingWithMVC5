using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;
using System;

namespace Models.Communities
{
    // Vous pouvez ajouter des données de profil pour l'utilisateur en ajoutant plus de propriétés à votre classe ApplicationUser ; consultez http://go.microsoft.com/fwlink/?LinkID=317594 pour en savoir davantage.
    public class NgCookingUser : IdentityUser
    {
        public string FirstName { get; set; } 
        public string SurName { get; set; }
        [Range(1, 3)]
        public int Level { get; set; }
        public Byte[] Picture { get; set; }
        public string City { get; set; }
        public DateTime Birth { get; set; }     
        public string Bio { get; set; }  
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<NgCookingUser> manager) 
        {
            // Notez qu'authenticationType doit correspondre à l'élément défini dans CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie); 
            // Ajouter les revendications personnalisées de l’utilisateur ici
            return userIdentity; 
        }  
    }
}