﻿using System;
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
        private IEnumerable<Ingredient> ingredintsList = new HashSet<Ingredient>();
        // GET: Recette
        public ActionResult Index()
        {
            System.Web.HttpContext.Current.Session["recetteIngs"] = new HashSet<Ingredient>(); 
            //A rajouter dans create get
            TempData["RecetteIngredientsList"] = ingredintsList; 
            return View(db.Recettes.ToList());
        }
        #region opertaions for posting a recette
        public IQueryable<Object> getAllCategories()
        {
            _ngCookingServices = new NgCookingServices(db);
            return _ngCookingServices.GetAll<Category>(_category); 
        }
        
        #endregion

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
      
        public JsonResult UpdateIngredientList(UpdateRecetteIngredientViewModel model)
        {
            var op = model.Operation; 
            var tempList = TempData["RecetteIngredientsList"];
            var ingsList= System.Web.HttpContext.Current.Session["recetteIngs"];
            var ing = db.Ingredients.Find(model.Id);
            switch (op)
            {
                case"add":
                    ((HashSet<Ingredient>)ingsList).Add(ing);    
                    break;
                case "remove":
                    ((HashSet<Ingredient>)ingsList).Remove(ing); 
                    //(tempList as HashSet<Ingredient>).Remove(ing);   
                    break;
                default:
                    break;
            }
            TempData["RecetteIngredientsList"] = tempList; 
            System.Web.HttpContext.Current.Session["recetteIngs"] = ingsList;  
            ViewBag.ingredientsToDisplay = ingsList;
            //return RedirectToAction("Create");
            //TempData["ingredients"] = ne; 
            //db.Ingredients.
            //return View(); 
            return Json("message");       
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
        [HttpPost]
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
