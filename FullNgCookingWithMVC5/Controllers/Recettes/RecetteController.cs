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

namespace FullNgCookingWithMVC5.Controllers
{
    public class RecetteController : Controller 
    {
        private NgCookingDbContext db = new NgCookingDbContext();

        // GET: Recette
        public ActionResult Index()
        {
            return View(db.Recettes.ToList());
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
            var catgories = db.Categories.OrderBy(c => c.Name).ToList();
            var categoriesNameList = new List<string>();
            foreach (var item in catgories)
            {
                categoriesNameList.Add(item.Name);
            }
            ViewBag.categories = categoriesNameList;
            ViewBag.itemByDefault = "Choose category";
            return View();
        }

        // POST: Recette/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,IsAvailable,Picture,Calories,Preparation,CreationDate,Category,CreatorId")] Recette recette)
        {
            if (ModelState.IsValid)
            {
                db.Recettes.Add(recette);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(recette);
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
