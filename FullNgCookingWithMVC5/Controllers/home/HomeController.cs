using Models.Ingredients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FullNgCookingWithMVC5.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index() 
        {
            System.Web.HttpContext.Current.Session["sortedCommunitiesList"] = null;
            System.Web.HttpContext.Current.Session["recetteIngs"] = new HashSet<Ingredient>();
            return View();  
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            var cntrl = RouteData.Values[""];

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";  
            return View();
        }
    }
}