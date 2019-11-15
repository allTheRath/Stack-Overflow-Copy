using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using QA_Project.Models;

namespace QA_Project.Controllers
{
    public class User_PostController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        //// GET: User_Post
        //public ActionResult Index()
        //{
        //    return View(db.All_Posts.ToList());
        //}

        //// GET: User_Post/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    User_Post user_Post = db.All_Posts.Find(id);
        //    if (user_Post == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(user_Post);
        //}

        // GET: User_Post/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: User_Post/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Discription,Post_Type")] User_Post user_Post)
        {
            user_Post.Acceptance_Of_Post = false;
            user_Post.Answered_Count = 0;
            user_Post.Associated_User_Id = User.Identity.GetUserId();
            user_Post.PostedOn = DateTime.Now;
            user_Post.Voted_Count = 0;

            if (ModelState.IsValid)
            {
                db.All_Posts.Add(user_Post);
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }

            return View(user_Post);
        }

        // GET: User_Post/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User_Post user_Post = db.All_Posts.Find(id);
            if (user_Post == null)
            {
                return HttpNotFound();
            }
            return View(user_Post);
        }

        // POST: User_Post/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Discription,Acceptance_Of_Post")] User_Post user_Post)
        {
            User_Post user_Post_Existing = db.All_Posts.Find(user_Post.Id);
            user_Post.PostedOn = DateTime.Now;
            user_Post.Voted_Count = user_Post_Existing.Voted_Count;
            user_Post.Post_Type = user_Post.Post_Type;
            user_Post.Answered_Count = user_Post_Existing.Answered_Count;
            
            if (ModelState.IsValid)
            {
                db.Entry(user_Post).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index","Home");
            }
            return View(user_Post);
        }

        // GET: User_Post/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User_Post user_Post = db.All_Posts.Find(id);
            if (user_Post == null)
            {
                return HttpNotFound();
            }
            return View(user_Post);
        }

        // POST: User_Post/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User_Post user_Post = db.All_Posts.Find(id);
            db.All_Posts.Remove(user_Post);
            db.SaveChanges();
            return RedirectToAction("Index","Home");
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
