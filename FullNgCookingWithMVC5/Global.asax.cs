using AutoMapper;
using FullNgCookingWithMVC5.Models;
using System;
using System.Collections.Generic;
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
                //config.AddProfile(new DomainToViewModelMappingProfile()); 
                config.CreateMap<RegisterViewModel, NgCookingUser>(); 
                config.CreateMap<NgCookingUser, RegisterViewModel>(); 
            });
        }
    } 

    public class DomainToViewModelMappingProfile : Profile
    {
        public override string ProfileName
        {
            get { return "DomainToViewModelMappings"; }
        }

        protected override void Configure()
        {
            //Mapper.CreateMap<NgCookingUser, RegisterViewModel>();
        }
    }
    public class ViewModelToDomainMappingProfile : Profile
    {
        public override string ProfileName
        {
            get { return "ViewModelToDomainMappings"; }
        }

        protected override void Configure()
        {
            //Mapper.CreateMap<RegisterViewModel, NgCookingUser>();
        }
    }

}
