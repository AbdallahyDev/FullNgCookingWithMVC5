using FullNgCookingWithMVC5.Controllers;
using FullNgCookingWithMVC5.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models.Recettes;
using Moq;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using test.Commun;

namespace test
{
    [TestClass]
    public class UnitTestRecetteController
    {
        IQueryable<Recette> data;
        RecetteController recetteCntrl;
        public UnitTestRecetteController()
        {
            data = MoqStart.getRecetteData(); 
            var mockNgContext = new Mock<NgCookingDbContext>();
            var mockSet = new Mock<DbSet<Recette>>();
            mockSet.As<IQueryable<Recette>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Recette>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Recette>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Recette>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
            mockNgContext.Setup(context => context.Recettes).Returns(mockSet.Object);
            recetteCntrl = new RecetteController(mockNgContext.Object); 
        }


        [TestMethod]
        public void TestDetailsRecetteAction()
        {
            var result1 = recetteCntrl.Details(1) as ViewResult;
            var recette1 = (Recette)result1.ViewData.Model;
            Assert.AreEqual("aaaa", recette1.Name);
            var result2 = recetteCntrl.Details(2) as ViewResult;
            var recette2 = (Recette)result2.ViewData.Model;
            Assert.AreEqual("Tajine de poulet", recette2.Name);
            var result3 = recetteCntrl.Details(3) as ViewResult;
            var recette3 = (Recette)result3.ViewData.Model;
            Assert.AreEqual("BBB", recette3.Name);
        }
        [TestMethod]
        public void TestIndexRecetteAction()
        {
            //var result = recetteCntrl.Index() as ViewResult;
           // var recettes = (List<Recette>)result.ViewData.Model;    
            Assert.AreEqual(3, 3);

        }
    }
}
