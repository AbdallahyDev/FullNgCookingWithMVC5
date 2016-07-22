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
using FullNgCookingWithMVC5.Services;
using Models.Ctegories;
using Microsoft.AspNet.Identity;
using System.Data.Entity.Validation;

namespace FullNgCookingWithMVC5.Controllers
{
    public class RecetteController : Controller
    {
        private NgCookingDbContext db = new NgCookingDbContext();
        private NgCookingServices _ngCookingServices;
        private Category _category = new Category();
        private Recette _recette = new Recette();
        private IEnumerable<Ingredient> ingredintsList = new HashSet<Ingredient>();
        private List<Recette> filtredRecette = new List<Recette>();
        public RecetteController(NgCookingDbContext db)
        {
            this.db = db;               
            _ngCookingServices = new NgCookingServices(db);        
        }
        public RecetteController()
        {

        }
        // GET: Recette
        public ActionResult Index()
        {
            System.Web.HttpContext.Current.Session["recetteIngs"] = new HashSet<Ingredient>();
            ViewBag.filtredRecette = (List<Recette>)System.Web.HttpContext.Current.Session["filtredRecette"];
            return View(db.Recettes.ToList());
        }


        #region opertaions for posting a recette
        public IQueryable<Object> getAllCategories()
        {
            _ngCookingServices = new NgCookingServices(db);
            return _ngCookingServices.GetAll<Category>(_category);
        }

        #endregion


        public IQueryable<Object> getAll()
        {
            return _ngCookingServices.GetAll<Recette>(_recette);
        }
        public float getRecetteNote(int idRecette)
        {
            return _ngCookingServices.getRecetteNote(idRecette);
        }

        public IQueryable<Recette> getBestRecettes()
        {
            var result = _ngCookingServices.GetBestRecettes();
            var i = result.Count();
            return _ngCookingServices.GetBestRecettes();
        }
        public IQueryable<Object> getNewRecettes()
        {
            return _ngCookingServices.GetNewRecettes();
        }

        [ActionName("AddIngToRecette")]
        ///Ingredient/GetIngByCategory
        public JsonResult GetIngByCategory(int idCategory)
        {
            try
            {

                var categorySelected = db.Categories.Find(idCategory);
                var ingredintsList = db.Ingredients
                       .Where(ing => ing.Category == categorySelected.Name).ToList().OrderBy(ing => ing.Name);
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

        #region The filters
        public List<Recette> FilterRecettteByIngs(string subIngName)
        {

            return null;
        }
        public List<Recette> FilterRecettteByCalorieValue(int minCalorieValue, int maxCalorieValue)
        {

            return null;
        }
        public JsonResult FilterRecettteByName(string subName)
        {
            try
            {
                return Json("", JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(e.Message);
            }

        }
        public JsonResult getFilteredRecettes(string subName = "", string ingsName = "", float minCalorieValue = 0, float maxCalorieValue = float.MaxValue, string orderBy = "")
        {

            try
            {
                var filteredRecettes = _ngCookingServices.getFilteredRecettes(subName, ingsName, minCalorieValue, maxCalorieValue, orderBy);        
                System.Web.HttpContext.Current.Session["filtredRecette"] = filteredRecettes;    
                return Json(filteredRecettes, JsonRequestBehavior.AllowGet);                    
            }
            catch (Exception e)
            {   
                return Json(e.Message);     
            }

        }
        #endregion

      

        [Route("Recette/Details/{id}")]
        // GET: Recette/Details/5
        public ActionResult Details(int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var idRecette = id;
            Recette recette = db.Recettes.FirstOrDefault(a => a.Id == id);
            if (recette == null)
            {
                return HttpNotFound();
            }
            ViewBag.sameRecettesList = getSameRecettes(recette);
            return View(recette);
        }

        private List<Recette> getSameRecettes(Recette recette)
        {
            var listRecette = db.Recettes.Where(x => (x.Calories - recette.Calories) < 50 && x.Category.Equals(recette.Category));
            return listRecette.ToList();    
        }

        #region Recettte creation
        // GET: Recette/Create
        public ActionResult Create()
        {
            ModelState.Clear();
            //A rajouter dans create get
            TempData["RecetteIngredientsList"] = new HashSet<Ingredient>();
            var categories = db.Categories.OrderBy(c => c.Name).ToList();
            var categoriesNameList = new List<string>();
            foreach (var item in categories)
            {
                categoriesNameList.Add(item.Name);
            }
            //ViewBag.categories = categoriesNameList;
            ViewBag.categories = categories;
            ViewBag.itemByDefault = "Choose category";
            ViewBag.recetteIngs = (HashSet<Ingredient>)System.Web.HttpContext.Current.Session["recetteIngs"];
            return View();
        }

        // POST: Recette/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RecetteViewModel recetteViewModel)
        {
            if (ModelState.IsValid)
            {
                HttpPostedFileBase image = Request.Files["image"];
                string currentUserId = User.Identity.GetUserId();
                var newRecette = Mapper.Map<Recette>(recetteViewModel);
                var category = db.Categories.Find(Int32.Parse(recetteViewModel.Category));
                newRecette.Category = category.Name;
                newRecette.CreatorId = currentUserId;
                newRecette.CreationDate = DateTime.Now.ToUniversalTime();
                if (image != null)
                {
                    newRecette.Picture = new byte[image.ContentLength];
                    image.InputStream.Read(newRecette.Picture, 0, image.ContentLength);
                }
                var ingList = (HashSet<Ingredient>)System.Web.HttpContext.Current.Session["recetteIngs"];
                foreach (var item in ingList)
                {
                    newRecette.Ingredients.Add(item);
                    newRecette.Calories += item.Calories;
                }
                db.Recettes.Add(newRecette);
                try
                {
                    db.SaveChanges();
                }
                catch (DbEntityValidationException e)
                {
                    foreach (var eve in e.EntityValidationErrors)
                    {
                        Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                            eve.Entry.Entity.GetType().Name, eve.Entry.State);
                        foreach (var ve in eve.ValidationErrors)
                        {
                            Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                                ve.PropertyName, ve.ErrorMessage);
                        }
                    }
                    throw;
                }

                return View();// RedirectToAction("Index"); 
            }
            return View(recetteViewModel);
        }

        public JsonResult UpdateIngredientList(UpdateRecetteIngredientViewModel model)
        {
            var op = model.Operation;
            var tempList = TempData["RecetteIngredientsList"];
            var ingsList = ((HashSet<Ingredient>)System.Web.HttpContext.Current.Session["recetteIngs"]);
            var ing = db.Ingredients.Find(model.Id);
            switch (op)
            {
                case "add":
                    ingsList.Add(ing);
                    break;
                case "remove":
                    ingsList.Remove(ing);
                    break;
                default:
                    break;
            }
            TempData["RecetteIngredientsList"] = tempList;
            System.Web.HttpContext.Current.Session["recetteIngs"] = ((HashSet<Ingredient>)ingsList);
            ViewBag.ingredientsToDisplay = ingsList;
            return Json("message");
        }
        #endregion

        #region Recettte edition
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
        #endregion

        #region Deletting of recettte
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
        #endregion

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
