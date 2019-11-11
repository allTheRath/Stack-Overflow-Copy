using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QA_Project.Models
{
    // All method signatures here.
    public interface IDataAccess
    {
        // Updating database methods below......
        void Add_Post(User_Post post);
        void Add_Tag(Tag tag);
        void Add_Post_Tag(Post_Tag post_Tag);

        // Geting data from database methods below........
        List<User_Post> GetAllPosts();
        List<User_Post> GetAllPostsByTag(int tag_id);
        List<User_Post> GetAllTagIdsContainingString(string searchString);
        List<Tag> GetAllTagsForPostId(int postId);
        List<User_Post> GetAllFollowedPostByPostId(int postId);
    }

    // All database accessible methods here.
    public class Application_DataAccess : IDataAccess
    {
        ApplicationDbContext db = new ApplicationDbContext();

        public void Add_Post(User_Post post)
        {
            db.All_Posts.Add(post);
            db.SaveChanges();

        }


        public void Add_Post_Tag(Post_Tag post_Tag)
        {
            db.Tag_Of_Post.Add(post_Tag);
            db.SaveChanges();
        }


        public void Add_Tag(Tag tag)
        {
            db.All_Tags.Add(tag);
            db.SaveChanges();
        }


        public List<User_Post> GetAllPosts()
        {
            return db.All_Posts.Where(x => x.Post_Type == Post_Type.Question).ToList();
        }

        public List<User_Post> GetAllPostsByTag(int tag_id)
        {
            var allPostsIds = db.Tag_Of_Post.Where(x => x.TagId == tag_id).ToList().Select(x => x.Post_Id);
            List<User_Post> All_Post_ByTag_Id = new List<User_Post>();
            foreach (var postId in allPostsIds)
            {
                User_Post post = db.All_Posts.Find(postId);
                All_Post_ByTag_Id.Add(post);
            }

            return All_Post_ByTag_Id;
        }

        public List<User_Post> GetAllTagIdsContainingString(string searchString)
        {
            List<int> allTagIds = new List<int>();

            allTagIds.AddRange(db.All_Tags.Where(x => x.Tag_Name.Contains(searchString) || searchString.Contains(x.Tag_Name)).ToList().Select(x => x.Id));
            List<User_Post> All_Post_ByTag_Id = new List<User_Post>();
            foreach (var tagid in allTagIds)
            {
                All_Post_ByTag_Id.AddRange(GetAllPostsByTag(tagid));
            }

            return All_Post_ByTag_Id.Distinct().ToList();
        }

        public List<Tag> GetAllTagsForPostId(int postId)
        {
            List<Tag> AllTags = new List<Tag>();
            var allTagIds = db.Tag_Of_Post.Where(x => x.Post_Id == postId).Select(x => x.TagId).ToList();
            foreach (var tagId in allTagIds)
            {
                Tag tag = db.All_Tags.Find(tagId);
                AllTags.Add(tag);
            }
            return AllTags;
        }


        public List<User_Post> GetAllFollowedPostByPostId(int postId)
        {
            List<User_Post> user_Posts = new List<User_Post>();
            var followed_post_ids = db.Followed_Posts.Where(x => x.Main_Post_Id == postId).ToList().OrderByDescending(x => x.Order).Select(x => x.Followed_Post_Id).ToList();
            foreach (var followed_post_id in followed_post_ids)
            {
                User_Post user_Post = db.All_Posts.Find(followed_post_id);
                user_Posts.Add(user_Post);
            }            
            return user_Posts;
        }
    }


}