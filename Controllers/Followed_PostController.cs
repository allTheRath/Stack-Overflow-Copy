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
    public class Followed_PostController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        //// GET: Followed_Post
        //public ActionResult Index()
        //{
        //    return View(db.Followed_Posts.ToList());
        //}

        //// GET: Followed_Post/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Followed_Post followed_Post = db.Followed_Posts.Find(id);
        //    if (followed_Post == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(followed_Post);
        //}

        //// GET: Followed_Post/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: Followed_Post/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "Id,Main_Post_Id,Followed_Post_Id,Order")] Followed_Post followed_Post)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Followed_Posts.Add(followed_Post);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(followed_Post);
        //}

        //// GET: Followed_Post/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Followed_Post followed_Post = db.Followed_Posts.Find(id);
        //    if (followed_Post == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(followed_Post);
        //}

        //// POST: Followed_Post/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "Id,Main_Post_Id,Followed_Post_Id,Order")] Followed_Post followed_Post)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(followed_Post).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(followed_Post);
        //}

        //// GET: Followed_Post/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Followed_Post followed_Post = db.Followed_Posts.Find(id);
        //    if (followed_Post == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(followed_Post);
        //}

        //// POST: Followed_Post/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Followed_Post followed_Post = db.Followed_Posts.Find(id);
        //    db.Followed_Posts.Remove(followed_Post);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

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
