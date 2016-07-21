using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using FullNgCookingWithMVC5.Models;
using Models.Communities;
using Models.Recettes;
using System.Collections.Generic;
using System;
using FullNgCookingWithMVC5.Services;

namespace FullNgCookingWithMVC5.Controllers
{
    public class CommunityController : Controller
    {
        private NgCookingDbContext db = new NgCookingDbContext();
        private NgCookingServices _ngCookingServices;
        public CommunityController()
        {
            _ngCookingServices = new NgCookingServices(db);
        }

        // GET: Community
        public ActionResult Index()
        {
            var sortedCommunitiesList = (List<NgCookingUser>)System.Web.HttpContext.Current.Session["sortedCommunitiesList"];
            ViewBag.allowedNumberToShow = System.Web.HttpContext.Current.Session["allowedNumberToShow"];
            return (sortedCommunitiesList!=null)? View(sortedCommunitiesList) : View(db.Users.ToList());            
        }

        public ActionResult UpdateAllowedNumber(int listSize, int allowedNumber)
        {
            try
            {
                if ((listSize - allowedNumber) >= 4)
                {
                    System.Web.HttpContext.Current.Session["allowedNumberToShow"] = allowedNumber + 4;
                }
                else
                {
                    System.Web.HttpContext.Current.Session["allowedNumberToShow"] = allowedNumber + (listSize - allowedNumber);
                }
            }
            catch (Exception e)
            {
                throw;
            }
            return RedirectToAction("Index");
        }
        public JsonResult OrderBy(string orderBy)
        {
            var communitiesList = db.Users.ToList();
            var id = communitiesList.First().Id;
            var result = _ngCookingServices.OrderCommunities(orderBy, communitiesList);
            System.Web.HttpContext.Current.Session["sortedCommunitiesList"] = result;
            try
            {
                return Json("", JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(e.Message);
            }
        }
        public List<Recette> getRecettesByUserId(string userId)
        {

            return _ngCookingServices.getRecettesByUserId(userId);

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
        public NgCookingUser GetUserById(string id)
        {
            if (id == null)
            {
                return null;
            }
            return db.Users.Find(id);
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
