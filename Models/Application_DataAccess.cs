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
        void Add_FollowedPost(Followed_Post followed_Post);
        void Add_User_Vote_Of_Post(User_Vote_Of_Post user_Vote_Of_Post);
        void Upvote(int post_id, string userid);
        void Downvote(int post_id, string userid);

        // Geting data from database methods below........
        List<User_Post> GetAllPosts();
        List<User_Post> GetAllPostsByTag(int tag_id);
        List<User_Post> GetAllPostsContainingString(string searchString);
        List<Tag> GetAllTagsForPostId(int postId);
        List<User_Post> GetAllFollowedPostByPostId(int postId);
        List<User_Post> GetAllFollowedCommentByPostId(int postId);
        User_Post GetPostById(int postId);
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

        public void Add_FollowedPost(Followed_Post followed_Post)
        {
            db.Followed_Posts.Add(followed_Post);
            db.SaveChanges();
        }

        public void Add_User_Vote_Of_Post(User_Vote_Of_Post user_Vote_Of_Post)
        {
            db.User_Vote_Of_Posts.Add(user_Vote_Of_Post);
            db.SaveChanges();
        }

        public List<User_Post> GetAllPosts()
        {
            return db.All_Posts.Where(x => x.Post_Type == Post_Type.Question).ToList().OrderByDescending(x => x.PostedOn).ToList();
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

            return All_Post_ByTag_Id.OrderByDescending(x => x.PostedOn).ToList();
        }

        public List<User_Post> GetAllPostsContainingString(string searchString)
        {
            List<int> allTagIds = new List<int>();

            allTagIds.AddRange(db.All_Tags.Where(x => x.Tag_Name.Contains(searchString) || searchString.Contains(x.Tag_Name)).ToList().Select(x => x.Id));
            List<User_Post> All_Post_ByTag_Id = new List<User_Post>();
            foreach (var tagid in allTagIds)
            {
                All_Post_ByTag_Id.AddRange(GetAllPostsByTag(tagid));
            }

            return All_Post_ByTag_Id.Distinct().ToList().OrderByDescending(x => x.PostedOn).ToList();
        }

        public List<Tag> GetAllTagsForPostId(int postId)
        {
            List<Tag> AllTags = new List<Tag>();
            var allTagIds = db.Tag_Of_Post.ToList().Where(x => x.Post_Id == postId);
            var allIds = allTagIds.ToList().Select(x => x.TagId).ToList();
            foreach (var tagId in allIds)
            {
                Tag tag = db.All_Tags.Find(tagId);
                if(tag.Tag_Name != null && tag.Tag_Name != "")
                {
                    AllTags.Add(tag);

                }
            }
            return AllTags;
        }


        public List<User_Post> GetAllFollowedPostByPostId(int postId)
        {
            List<User_Post> user_Posts = new List<User_Post>();
            var followed_post_ids = db.Followed_Posts.Where(x => x.Main_Post_Id == postId).ToList();
            var fPostIds = followed_post_ids.OrderByDescending(x => x.Order).Select(x => x.Followed_Post_Id).ToList();
            foreach (var followed_post_id in fPostIds)
            {
                User_Post user_Post = db.All_Posts.Find(followed_post_id);
                if (user_Post.Post_Type == Post_Type.Answer)
                {
                    user_Posts.Add(user_Post);
                }
            }
            return user_Posts.OrderByDescending(x => x.PostedOn).ToList();
        }

        public List<User_Post> GetAllFollowedCommentByPostId(int postId)
        {
            List<User_Post> user_Posts = new List<User_Post>();
            var followed_post_ids = db.Followed_Posts.Where(x => x.Main_Post_Id == postId).ToList();
            var fPostIds = followed_post_ids.OrderByDescending(x => x.Order).Select(x => x.Followed_Post_Id).ToList();
            foreach (var followed_post_id in fPostIds)
            {
                User_Post user_Post = db.All_Posts.Find(followed_post_id);
                if (user_Post.Post_Type == Post_Type.Comment)
                {
                    user_Posts.Add(user_Post);
                }
            }
            return user_Posts.OrderByDescending(x => x.PostedOn).ToList();
        }

        public User_Post GetPostById(int postId)
        {
            return db.All_Posts.Find(postId);
        }

        public void Upvote(int post_id, string userid)
        {
            var post = db.All_Posts.Find(post_id);
            var exist = db.User_Vote_Of_Posts.Where(x => x.Post_Id == post_id && x.Voter_Id == userid).FirstOrDefault();
            if (exist != null)
            {
                if (exist.Voted_Type == Voted_Type.UpVoted)
                {
                    return;
                }

                db.User_Vote_Of_Posts.Remove(exist);
                db.SaveChanges();
                post.Voted_Count -= 1;
            }

            User_Vote_Of_Post user_Vote_Of_Post = new User_Vote_Of_Post();
            user_Vote_Of_Post.Voter_Id = userid;
            user_Vote_Of_Post.Voted_Type = Voted_Type.UpVoted;
            user_Vote_Of_Post.Post_Id = post_id;
            db.User_Vote_Of_Posts.Add(user_Vote_Of_Post);
            post.Voted_Count += 1;
            db.SaveChanges();

        }

        public void Downvote(int post_id, string userid)
        {
            var post = db.All_Posts.Find(post_id);
            var exist = db.User_Vote_Of_Posts.Where(x => x.Post_Id == post_id && x.Voter_Id == userid).FirstOrDefault();
            if (exist != null)
            {
                if (exist.Voted_Type == Voted_Type.DownVoted)
                {
                    return;
                }

                db.User_Vote_Of_Posts.Remove(exist);
                db.SaveChanges();
            }

            User_Vote_Of_Post user_Vote_Of_Post = new User_Vote_Of_Post();
            user_Vote_Of_Post.Voter_Id = userid;
            user_Vote_Of_Post.Voted_Type = Voted_Type.DownVoted;
            user_Vote_Of_Post.Post_Id = post_id;
            db.User_Vote_Of_Posts.Add(user_Vote_Of_Post);
            db.SaveChanges();
        }
    }


}