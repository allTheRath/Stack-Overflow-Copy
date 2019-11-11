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
    public class Post_TagController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Post_Tag
        public ActionResult Index()
        {
            var tag_Of_Post = db.Tag_Of_Post.Include(p => p.Tag);
            return View(tag_Of_Post.ToList());
        }

        // GET: Post_Tag/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post_Tag post_Tag = db.Tag_Of_Post.Find(id);
            if (post_Tag == null)
            {
                return HttpNotFound();
            }
            return View(post_Tag);
        }

        // GET: Post_Tag/Create
        public ActionResult Create()
        {
            ViewBag.TagId = new SelectList(db.All_Tags, "Id", "Tag_Name");
            return View();
        }

        // POST: Post_Tag/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,TagId,Post_Id")] Post_Tag post_Tag)
        {
            if (ModelState.IsValid)
            {
                db.Tag_Of_Post.Add(post_Tag);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TagId = new SelectList(db.All_Tags, "Id", "Tag_Name", post_Tag.TagId);
            return View(post_Tag);
        }

        // GET: Post_Tag/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post_Tag post_Tag = db.Tag_Of_Post.Find(id);
            if (post_Tag == null)
            {
                return HttpNotFound();
            }
            ViewBag.TagId = new SelectList(db.All_Tags, "Id", "Tag_Name", post_Tag.TagId);
            return View(post_Tag);
        }

        // POST: Post_Tag/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,TagId,Post_Id")] Post_Tag post_Tag)
        {
            if (ModelState.IsValid)
            {
                db.Entry(post_Tag).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TagId = new SelectList(db.All_Tags, "Id", "Tag_Name", post_Tag.TagId);
            return View(post_Tag);
        }

        // GET: Post_Tag/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post_Tag post_Tag = db.Tag_Of_Post.Find(id);
            if (post_Tag == null)
            {
                return HttpNotFound();
            }
            return View(post_Tag);
        }

        // POST: Post_Tag/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Post_Tag post_Tag = db.Tag_Of_Post.Find(id);
            db.Tag_Of_Post.Remove(post_Tag);
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
