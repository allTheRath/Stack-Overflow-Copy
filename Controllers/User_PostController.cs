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
                if (user_Post.Post_Type == Post_Type.Question)
                {
                    User_Post post = db.All_Posts.Where(x => x.Discription == user_Post.Discription).FirstOrDefault();
                    if (post != null)
                    {
                        return RedirectToAction("AddOrCreateTags", new { postId = post.Id });
                    }
                    else
                    {
                        // an error page link here..
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    return RedirectToAction("Index", "Home");

                }
            }

            return View(user_Post);
        }


        public ActionResult AddOrCreateTags(int? postId)
        {
            if (postId == null)
            {
                return RedirectToAction("Index", "Home");
            }
            User_Post question = db.All_Posts.Find(postId);
            var model = new AddTagsViewmodel();
            model.SelectedTagIds = new List<int>();
            model.PostId = question.Id;
            model.PostedOn = question.PostedOn;
            model.Title = question.Title;
            model.Discription = question.Discription;
            model.Answered_Count = question.Answered_Count;
            model.Voted_Count = question.Voted_Count;
            var tagids = db.Tag_Of_Post.ToList().Where(x => x.Post_Id == question.Id).ToList().Select(x => x.TagId).ToList();
            model.Tags = db.All_Tags.ToList().Where(x => (!tagids.Contains(x.Id))).ToList();


            if (tagids.Count() > 0)
            {
                model.AlreadySelectedTags = new List<Tag>();
            }
            else
            {
                List<Tag> alreadySelectedTags = new List<Tag>();
                tagids.ForEach(x =>
                {
                    alreadySelectedTags.Add(db.All_Tags.Find(x));
                });

                model.AlreadySelectedTags = alreadySelectedTags;
            }


            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateTag(int? PostId, string tag_name)
        {
            var post = db.All_Posts.Find(PostId);
            if (post != null && tag_name != "")
            {
                Tag tag = new Tag();
                tag.Tag_Name = tag_name;
                db.All_Tags.Add(tag);
                db.SaveChanges();
                Post_Tag post_Tag = new Post_Tag();
                Tag added = db.All_Tags.Where(x => x.Tag_Name == tag_name).FirstOrDefault();
                post_Tag.Post_Id = post.Id;
                post_Tag.TagId = added.Id;
                db.Tag_Of_Post.Add(post_Tag);
                db.SaveChanges();
            }

            return RedirectToAction("AddOrCreateTags", new { postId = post.Id });
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddTags(int? PostId, List<int> SelectedTagIds)
        {
            var post = db.All_Posts.Find(PostId);
            if (post != null)
            {
                Tag t = db.All_Tags.Find(SelectedTagIds[0]);
                Post_Tag post_Tag = new Post_Tag();
                post_Tag.Post_Id = post.Id;
                post_Tag.TagId = t.Id;
                db.Tag_Of_Post.Add(post_Tag);
                db.SaveChanges();
            }

            return RedirectToAction("AddOrCreateTags", new { postId = post.Id });
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
            user_Post_Existing.Title = user_Post.Title;
            user_Post_Existing.Discription = user_Post.Discription;
            if (User.Identity.IsAuthenticated)
            {
                // if user is the actual creator of this question then he can accept the question. as question is been answered.
                if (User.Identity.GetUserId() == user_Post_Existing.Associated_User_Id)
                {
                    user_Post_Existing.Acceptance_Of_Post = user_Post.Acceptance_Of_Post;
                    
                }
                db.SaveChanges();
            }


            if (user_Post.Post_Type == Post_Type.Question)
            {
                User_Post post = db.All_Posts.Where(x => x.Discription == user_Post.Discription).FirstOrDefault();
                if (post != null)
                {
                    return RedirectToAction("AddOrCreateTags", new { postId = post.Id });
                }
                else
                {
                    // an error page link here..
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                return RedirectToAction("Index", "Home");

            }
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
            return RedirectToAction("Index", "Home");
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
