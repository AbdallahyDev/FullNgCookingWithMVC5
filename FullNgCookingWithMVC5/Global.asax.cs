using AutoMapper;
using FullNgCookingWithMVC5.Models;
using Models.Users;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

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

            }); 
        } 
    }

}
