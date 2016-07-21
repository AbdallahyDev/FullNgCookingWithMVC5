using FullNgCookingWithMVC5.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models.Recettes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace test
{
    [TestClass]
    class RecetteControllerTest
    {
        RecetteController recetteCntrl = new RecetteController();
        [TestMethod]
        public void TestDetailsViewData()
        {

            var result = recetteCntrl.Details(2) as ViewResult;
            var recette = (Recette)result.ViewData.Model;
            Assert.AreEqual("Tajine de poulet", recette.Name);
        }

    }
}
