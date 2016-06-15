using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FullNgCookingWithMVC5.Controllers
{
    public class MyController : Controller
    {
        // GET: My
        public ActionResult HelloWorld()
        {
            return View();
        }
    }
}