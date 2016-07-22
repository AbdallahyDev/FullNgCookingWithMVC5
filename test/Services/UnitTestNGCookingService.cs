using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FullNgCookingWithMVC5.Models;
using Models.Recettes;
using System.Linq;
using System.Data.Entity;
using Moq;
using test.Commun;
using FullNgCookingWithMVC5.Services;

namespace test.Services
{
    [TestClass]
    public class UnitTestNGCookingService
    {
        NgCookingServices ngCookingService;
        public UnitTestNGCookingService()
        {
            
            var data = MoqStart.getRecetteData();
            var mockNgContext = new Mock<NgCookingDbContext>();
            var mockSet = new Mock<DbSet<Recette>>();
            mockSet.As<IQueryable<Recette>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Recette>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Recette>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Recette>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
            mockNgContext.Setup(context => context.Recettes).Returns(mockSet.Object);
            ngCookingService = new NgCookingServices(mockNgContext.Object);

        }
        [TestMethod]
        public void TestMethodFindById()
        {
            var recette = ngCookingService.FindById(1, "Recette");
            Assert.AreEqual("aaaa",((Recette)recette).Name);  
        }
        [TestMethod]
        public void TestMethodgetRecettesByUserId()
        {
            var recette = ngCookingService.getRecettesByUserId("gggg");
            var firstRecette = recette.First();
            Assert.AreEqual("aaaa", ((Recette)firstRecette).Name);      
        }
    }
}
