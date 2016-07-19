using AutoMapper;
using FullNgCookingWithMVC5.Models;
using FullNgCookingWithMVC5.ViewModels.Comments;
using Microsoft.AspNet.Identity;
using Model.Comments;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace FullNgCookingWithMVC5.Controllers.Comments
{
    public class CommentController : Controller
    {
        private NgCookingDbContext db = new NgCookingDbContext();
        // GET: Comment
        public ActionResult Post(CommentViewModel commentViewModel)
        {
            if (ModelState.IsValid)
            {
                var newComment = Mapper.Map<Comment>(commentViewModel);
                string currentUserId = User.Identity.GetUserId();
                newComment.UserId = currentUserId;
                try
                {
                    db.Comments.Add(newComment);
                    db.SaveChanges();   
                }
                catch (DbEntityValidationException e)
                {
                    
                    return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
                }
            }
            return RedirectToAction("Index", "Home");
        }
    }
}