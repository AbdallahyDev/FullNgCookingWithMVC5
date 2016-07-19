using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FullNgCookingWithMVC5.Models;
using Models.Ingredients;
using ViewModels.Ingredients;
using AutoMapper;
using FullNgCookingWithMVC5.Services;

namespace FullNgCookingWithMVC5.Controllers
{
    public class IngredientController : Controller
    {
        private NgCookingDbContext db = new NgCookingDbContext();
        private NgCookingServices _ngCookingServices;
        private Ingredient _ingredient = new Ingredient();
        public IngredientController()
        {
            _ngCookingServices = new NgCookingServices(db);
        }
        // GET: Ingredient
        public ActionResult Index()
        {
            ViewBag.filteredIngredients =  (List<Ingredient>)System.Web.HttpContext.Current.Session["filteredIngredients"];
            return View(db.Ingredients.ToList());
        }
        public JsonResult getFilteredIngredients(string subIngName = "", string categorie = "", float minCalorieValue = 0, float maxCalorieValue = float.MaxValue)
        {

            try
            {
                var filteredIngredients = _ngCookingServices.getFilteredIngredients(subIngName, categorie, minCalorieValue, maxCalorieValue);
                System.Web.HttpContext.Current.Session["filteredIngredients"] = filteredIngredients; 
                return Json(filteredIngredients, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(e.Message);
            }

        }
        public IQueryable<Ingredient> getIngsByCategory(int categoryId)
        {
            return _ngCookingServices.getIngsByCategory(categoryId);
        }
        public Ingredient getIngsById(int ingId) 
        {
            return ((Ingredient)_ngCookingServices.FindById(ingId,"Ingredient"));
        }
        public IQueryable<Object> getAll()
        {
            _ngCookingServices = new NgCookingServices(db);
            return _ngCookingServices.GetAll<Ingredient>(_ingredient);
        }

        // GET: Ingredient/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ingredient ingredient = db.Ingredients.Find(id);
            if (ingredient == null)
            {
                return HttpNotFound();
            }
            return View(ingredient);
        }

        // GET: Ingredient/Create
        public ActionResult Create()
        {
            var catgories = db.Categories.OrderBy(c=>c.Name).ToList();
            var categoriesNameList = new List<string>();
            foreach (var item in catgories) 
            {
                categoriesNameList.Add(item.Name); 
            }
            ViewBag.categories = categoriesNameList;
            ViewBag.itemByDefault = "Choose category";
            return View();
        }

        // POST: Ingredient/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( IngredientViewModel ingredientViewModel, HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                var newIngredient = Mapper.Map<Ingredient>(ingredientViewModel);
                if (image != null)
                {
                    newIngredient.Picture = new byte[image.ContentLength];
                    image.InputStream.Read(newIngredient.Picture, 0, image.ContentLength);
                }
                db.Ingredients.Add(newIngredient); 
                db.SaveChanges();
                return RedirectToAction("Index");   
            }

            return View(ingredientViewModel);
        }
      

        // GET: Ingredient/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);         
            }
            Ingredient ingredient = db.Ingredients.Find(id);            
            if (ingredient == null)
            {
                return HttpNotFound();      
            }
            return View(ingredient);
        }

        // POST: Ingredient/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,IsAvailable,Picture,Calories,Category")] Ingredient ingredient)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ingredient).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(ingredient);
        }

        // GET: Ingredient/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ingredient ingredient = db.Ingredients.Find(id);
            if (ingredient == null)
            {
                return HttpNotFound();
            }
            return View(ingredient);
        }

        // POST: Ingredient/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Ingredient ingredient = db.Ingredients.Find(id);
            db.Ingredients.Remove(ingredient);
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
