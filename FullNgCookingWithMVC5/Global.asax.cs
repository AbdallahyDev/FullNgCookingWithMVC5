using AutoMapper;
using FullNgCookingWithMVC5.Models;
using Models.Communities;
using Models.Ingredients;
using Models.Recettes;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using ViewModels.Ingredients;
using ViewModels.Recettes;

namespace FullNgCookingWithMVC5
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AutoMapperWebConfiguration.Configure();
        }
    }
    public static class AutoMapperWebConfiguration
    {
        public static void Configure()
        {
            Mapper.Initialize(config => 
            {
                config.CreateMap<RegisterViewModel, NgCookingUser>(); 
                config.CreateMap<NgCookingUser, RegisterViewModel>();  
                config.CreateMap<NgCookingUser, IndexViewModel>();   
                config.CreateMap<IngredientViewModel, Ingredient>();
                config.CreateMap<RecetteViewModel, Recette>();   
            }); 
        } 
    }

}
