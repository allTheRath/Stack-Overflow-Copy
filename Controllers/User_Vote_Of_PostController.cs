using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QA_Project.Models;

namespace QA_Project.Controllers
{
    public class User_Vote_Of_PostController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: User_Vote_Of_Post
        public ActionResult Index()
        {
            return View(db.User_Vote_Of_Posts.ToList());
        }

        // GET: User_Vote_Of_Post/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User_Vote_Of_Post user_Vote_Of_Post = db.User_Vote_Of_Posts.Find(id);
            if (user_Vote_Of_Post == null)
            {
                return HttpNotFound();
            }
            return View(user_Vote_Of_Post);
        }

        // GET: User_Vote_Of_Post/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: User_Vote_Of_Post/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Voter_Id,Voted_Type,Post_Id")] User_Vote_Of_Post user_Vote_Of_Post)
        {
            if (ModelState.IsValid)
            {
                db.User_Vote_Of_Posts.Add(user_Vote_Of_Post);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(user_Vote_Of_Post);
        }

        // GET: User_Vote_Of_Post/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User_Vote_Of_Post user_Vote_Of_Post = db.User_Vote_Of_Posts.Find(id);
            if (user_Vote_Of_Post == null)
            {
                return HttpNotFound();
            }
            return View(user_Vote_Of_Post);
        }

        // POST: User_Vote_Of_Post/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Voter_Id,Voted_Type,Post_Id")] User_Vote_Of_Post user_Vote_Of_Post)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user_Vote_Of_Post).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user_Vote_Of_Post);
        }

        // GET: User_Vote_Of_Post/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User_Vote_Of_Post user_Vote_Of_Post = db.User_Vote_Of_Posts.Find(id);
            if (user_Vote_Of_Post == null)
            {
                return HttpNotFound();
            }
            return View(user_Vote_Of_Post);
        }

        // POST: User_Vote_Of_Post/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User_Vote_Of_Post user_Vote_Of_Post = db.User_Vote_Of_Posts.Find(id);
            db.User_Vote_Of_Posts.Remove(user_Vote_Of_Post);
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
