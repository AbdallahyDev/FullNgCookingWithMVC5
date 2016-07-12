﻿using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using FullNgCookingWithMVC5.Models;
using Models.Communities;
using Models.Recettes;
using System.Collections.Generic;

namespace FullNgCookingWithMVC5.Controllers
{
    public class CommunityController : Controller
    {
        private NgCookingDbContext db = new NgCookingDbContext();

        // GET: Community
        public ActionResult Index()
        {
            return View(db.Users.ToList());    
        }
        public List<Recette> getRecettesByUserId(string userId)
        {
            if (userId==null)
            {
                return null;
            }
            var userRecettes = db.Recettes.Where(x => x.CreatorId.Equals(userId));
            return userRecettes.ToList();

        }
        // GET: Community/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var ngCookingUser = db.Users.Find(id);
            if (ngCookingUser == null)
            {
                return HttpNotFound();
            }
            return View(ngCookingUser);
        }

        // GET: Community/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Community/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Firstname,Surname,Level,Picture,City,Birth,Bio,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName")] NgCookingUser ngCookingUser)
        {
            if (ModelState.IsValid)
            {
                //db.NgCookingUsers.Add(ngCookingUser);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(ngCookingUser);
        }

        // GET: Community/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NgCookingUser ngCookingUser = db.Users.Find(id); 
            if (ngCookingUser == null)
            {
                return HttpNotFound();
            }
            return View(ngCookingUser);
        }

        // POST: Community/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Firstname,Surname,Level,Picture,City,Birth,Bio,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName")] NgCookingUser ngCookingUser)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ngCookingUser).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(ngCookingUser);
        }

        // GET: Community/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NgCookingUser ngCookingUser = db.Users.Find(id);
            if (ngCookingUser == null)
            {
                return HttpNotFound();
            }
            return View(ngCookingUser);
        }

        // POST: Community/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            NgCookingUser ngCookingUser = db.Users.Find(id);
            //db.NgCookingUsers.Remove(ngCookingUser);
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
