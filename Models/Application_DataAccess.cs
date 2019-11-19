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
        void AddUserProfileInfo(string imageUrl, string screenName, string userId);

        // Geting data from database methods below........
        User_Post GetPostByDiscription(string discription);
        List<User_Post> GetAllPosts();
        List<User_Post> GetAllPostsByTag(int tag_id);
        List<User_Post> GetAllPostsContainingString(string searchString);
        List<Tag> GetAllTagsForPostId(int postId);
        List<User_Post> GetAllFollowedPostByPostId(int postId);
        List<User_Post> GetAllFollowedCommentByPostId(int postId);
        User_Post GetPostById(int postId);
        int GetLastOrderOfFollowedPost(int postId);
        AddUserInfoViewmodel GetUserInfoByPostId(int postid);
    }

    // All database accessible methods here.
    public class Application_DataAccess : IDataAccess
    {
        ApplicationDbContext db = new ApplicationDbContext();

        public void Add_Post(User_Post post)
        {
            post.Comment_Count = 0;
            post.View_Count = 0;
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

            User_Post mainPost = db.All_Posts.Find(followed_Post.Main_Post_Id);
            User_Post followedPost = db.All_Posts.Find(followed_Post.Followed_Post_Id);
            if (followedPost.Post_Type == Post_Type.Answer)
            {
                mainPost.Answered_Count += 1;
                db.SaveChanges();
            }
            else if (followedPost.Post_Type == Post_Type.Comment)
            {
                if (mainPost.Comment_Count == null)
                {
                    mainPost.Comment_Count = 0;
                }
                mainPost.Comment_Count += 1;
                db.SaveChanges();
            }
        }

        public void Add_User_Vote_Of_Post(User_Vote_Of_Post user_Vote_Of_Post)
        {
            db.User_Vote_Of_Posts.Add(user_Vote_Of_Post);
            db.SaveChanges();
        }

        public List<User_Post> GetAllPosts()
        {
            var allposts = db.All_Posts.Where(x => x.Post_Type == Post_Type.Question).ToList();

            if (allposts != null)
            {
                return allposts.OrderByDescending(x => x.PostedOn).ToList();
            }
            else
            {
                return new List<User_Post>();
            }
        }

        public List<User_Post> GetAllPostsByTag(int tag_id)
        {
            var allPostsIds = db.Tag_Of_Post.Where(x => x.TagId == tag_id).ToList().Select(x => x.Post_Id);
            List<User_Post> All_Post_ByTag_Id = new List<User_Post>();
            foreach (var postId in allPostsIds)
            {
                User_Post post = db.All_Posts.Find(postId);
                if (post != null)
                {
                    All_Post_ByTag_Id.Add(post);
                }
            }
            if (All_Post_ByTag_Id != null)
            {
                return All_Post_ByTag_Id.OrderByDescending(x => x.PostedOn).ToList();

            }
            else
            {
                return new List<User_Post>();
            }
        }

        public List<User_Post> GetAllPostsContainingString(string searchString)
        {
            List<int> allTagIds = new List<int>();

            allTagIds.AddRange(db.All_Tags.Where(x => x.Tag_Name.Contains(searchString) || searchString.Contains(x.Tag_Name)).ToList().Select(x => x.Id));
            List<User_Post> All_Post_ByTag_Id = new List<User_Post>();
            foreach (var tagid in allTagIds)
            {
                List<User_Post> posts = GetAllPostsByTag(tagid);
                if (posts != null)
                {
                    All_Post_ByTag_Id.AddRange(posts);
                }
            }
            if (All_Post_ByTag_Id != null)
            {
                return All_Post_ByTag_Id.Distinct().ToList().OrderByDescending(x => x.PostedOn).ToList();

            }
            else
            {
                return new List<User_Post>();
            }
        }

        public List<Tag> GetAllTagsForPostId(int postId)
        {
            List<Tag> AllTags = new List<Tag>();
            var allTagIds = db.Tag_Of_Post.ToList().Where(x => x.Post_Id == postId);
            var allIds = allTagIds.ToList().Select(x => x.TagId).ToList();
            foreach (var tagId in allIds)
            {
                Tag tag = db.All_Tags.Find(tagId);
                if (tag.Tag_Name != null && tag.Tag_Name != "")
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
                if (user_Post != null && user_Post.Post_Type == Post_Type.Answer)
                {
                    user_Posts.Add(user_Post);
                }
            }

            if (user_Posts != null)
            {
                return user_Posts.OrderByDescending(x => x.PostedOn).ToList();
            }
            else
            {
                return new List<User_Post>();
            }
        }

        public List<User_Post> GetAllFollowedCommentByPostId(int postId)
        {
            List<User_Post> user_Posts = new List<User_Post>();
            var followed_post_ids = db.Followed_Posts.Where(x => x.Main_Post_Id == postId).ToList();
            var fPostIds = followed_post_ids.OrderByDescending(x => x.Order).Select(x => x.Followed_Post_Id).ToList();
            foreach (var followed_post_id in fPostIds)
            {
                User_Post user_Post = db.All_Posts.Find(followed_post_id);
                if (user_Post != null)
                {
                    if (user_Post.Post_Type == Post_Type.Comment)
                    {
                        if(user_Post != null)
                        {
                            user_Posts.Add(user_Post);

                        }
                    }
                }

            }
            if(user_Posts != null)
            {
                return user_Posts.OrderByDescending(x => x.PostedOn).ToList();
            }
            else
            {
                return new List<User_Post>();
            }
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
            var user = db.Users.Find(post.Associated_User_Id);
            user.Reputation += 5;
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
            var user = db.Users.Find(post.Associated_User_Id);
            user.Reputation -= 5;
            db.SaveChanges();
        }

        public User_Post GetPostByDiscription(string discription)
        {
            return db.All_Posts.Where(x => x.Discription == discription).FirstOrDefault();
        }

        public int GetLastOrderOfFollowedPost(int postId)
        {
            var allFollowedPost = db.Followed_Posts.Where(x => x.Main_Post_Id == postId).ToList();
            if (allFollowedPost != null)
            {
                return allFollowedPost.Count() + 1;
            }
            else
            {
                return 1;
            }
        }

        public void AddUserProfileInfo(string imageUrl, string screenName, string userId)
        {
            ApplicationUser user = db.Users.Find(userId);
            if (user == null)
            {
                return;
            }

            user.ProfileImageUrl = imageUrl;
            db.SaveChanges();
        }


        public AddUserInfoViewmodel GetUserInfoByPostId(int postid)
        {
            AddUserInfoViewmodel addUserInfoViewmodel = new AddUserInfoViewmodel();
            User_Post post = db.All_Posts.Find(postid);
            var uid = post.Associated_User_Id;
            ApplicationUser user = db.Users.Find(uid);
            addUserInfoViewmodel.Badge_Count = 0;
            if (user.ProfileImageUrl == null)
            {
                addUserInfoViewmodel.Profile_Url = "";

            }
            else
            {
                addUserInfoViewmodel.Profile_Url = user.ProfileImageUrl;
            }
            addUserInfoViewmodel.Reputation = user.Reputation;
            user.DisplayName = user.Email.Split('@')[0];
            db.SaveChanges();
            addUserInfoViewmodel.U_Name = user.DisplayName;
            addUserInfoViewmodel.User_Badge_Type = user.User_Badge;

            return addUserInfoViewmodel;
        }
    }


}