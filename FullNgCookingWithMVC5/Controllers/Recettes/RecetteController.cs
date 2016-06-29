using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FullNgCookingWithMVC5.Models;
using Models.Recettes;
using Models.Ingredients;
using FullNgCookingWithMVC5.ViewModels.Recettes;
using ViewModels.Recettes;
using AutoMapper;

namespace FullNgCookingWithMVC5.Controllers
{
    public class RecetteController : Controller
    {
        private NgCookingDbContext db = new NgCookingDbContext();
        private IEnumerable<Ingredient> ingredintsList;
        // GET: Recette
        public ActionResult Index()
        {
            ingredintsList = new HashSet<Ingredient>();
            TempData["RecetteingredientsList"] = ingredintsList;
            return View(db.Recettes.ToList());
        }

        
        [ActionName("AddIngToRecette")]
        ///Ingredient/GetIngByCategory
        public JsonResult GetIngByCategory(int idCategory)
        {
            try
            {

                var categorySelected = db.Categories.Find(idCategory);
                var ingredintsList = db.Ingredients
                       .Where(ing => ing.Category == categorySelected.Name).ToList().OrderBy(ing=>ing.Name);
                //var b = ingredintsList.Count(); 
                //ViewBag.ingredientsToDisplay = ingredintsList.Select(ing=>ing.Name);
                //TempData["ingredientsList"] = ingredintsList.Select(ing => ing.Name);
                //db.Ingredients.
                return Json(ingredintsList, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(e.Message);
            }
           
        }
      
        public ActionResult UpdateIngredientList(UpdateRecetteIngredientViewModel model)
        {
            var op = model.Operation;
            var tempList = TempData["RecetteingredientsList"];
            var ing = db.Ingredients.Find(model.Id);
            switch (op)
            {
                case"add":
                    (tempList as HashSet<Ingredient>).Add(ing);  
                    break;
                case "remove":
                    (tempList as HashSet<Ingredient>).Remove(ing);
                    break;
                default:
                    break;
            }
            TempData["RecetteingredientsList"] = tempList;
            ViewBag.ingredientsToDisplay = tempList;
            //TempData["ingredients"] = ne; 
            //db.Ingredients.
            return View(); 
        }
        // GET: Recette/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Recette recette = db.Recettes.Find(id);
            if (recette == null)
            {
                return HttpNotFound();
            }
            return View(recette);
        }

        // GET: Recette/Create
        public ActionResult Create()
        {
            var categories = db.Categories.OrderBy(c => c.Name).ToList();

            var categoriesNameList = new List<string>();
            foreach (var item in categories)
            {
                categoriesNameList.Add(item.Name);
            }
            //ViewBag.categories = categoriesNameList;
            ViewBag.categories = categories;
            ViewBag.itemByDefault = "Choose category";
            return View();
        }

        // POST: Recette/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RecetteViewModel recetteViewModel, HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                var newRecette = Mapper.Map<Recette>(recetteViewModel); 
                if (image != null)
                {
                    newRecette.Picture = new byte[image.ContentLength];
                    image.InputStream.Read(newRecette.Picture, 0, image.ContentLength); 
                }
                db.Recettes.Add(newRecette); 
                db.SaveChanges();
                return RedirectToAction("Index"); 
            } 
            return View(recetteViewModel); 
        }

        // GET: Recette/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Recette recette = db.Recettes.Find(id);
            if (recette == null)
            {
                return HttpNotFound();
            }
            return View(recette);
        }

        // POST: Recette/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,IsAvailable,Picture,Calories,Preparation,CreationDate,Category,CreatorId")] Recette recette)
        {
            if (ModelState.IsValid)
            {
                db.Entry(recette).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(recette);
        }

        // GET: Recette/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Recette recette = db.Recettes.Find(id);
            if (recette == null)
            {
                return HttpNotFound();
            }
            return View(recette);
        }

        // POST: Recette/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Recette recette = db.Recettes.Find(id);
            db.Recettes.Remove(recette);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
