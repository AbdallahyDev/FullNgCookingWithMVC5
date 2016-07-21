﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace FullNgCookingWithMVC5
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
              name: "home",
              url: "Home",
              defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
           );
            routes.MapRoute(
               name: "Acceuil",
               url: "{controller}/{action}",
               defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
           );
            routes.MapRoute(
             name: "RecetteDetails",
             url: "Community/Recette/Details",
             defaults: new { controller = "Recette", action = "Details", id = UrlParameter.Optional }
         );
            routes.MapRoute(
           name: "CommunityDetails",
           url: "Recette/Community/Details/{id}",
           defaults: new { controller = "Community", action = "Details", id = UrlParameter.Optional }
       );
            routes.MapRoute(
        name: "DetailsRecetteInCommunityDetails",
        url: "Recette/Community/Recette/Details/{id}",
        defaults: new { controller = "Recette", action = "Details", id = UrlParameter.Optional }
    );
            routes.MapRoute(
                name: "Default", 
                url: "",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

        }
    }
}
