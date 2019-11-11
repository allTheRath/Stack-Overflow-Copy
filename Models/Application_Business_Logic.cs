using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QA_Project.Models
{
    // An encapsulating class with associated method access from data access class.
    public class Application_Business_Logic
    {
        IDataAccess dataAccess;

        public Application_Business_Logic(IDataAccess dataAccess)
        {
            this.dataAccess = dataAccess;
        }

        // All access end points for database methods here..
        public void Add_Post(User_Post post)
        {
            this.dataAccess.Add_Post(post);
        }


        public void Add_Post_Tag(Post_Tag post_Tag)
        {
            this.dataAccess.Add_Post_Tag(post_Tag);
        }


        public void Add_Tag(Tag tag)
        {
            this.dataAccess.Add_Tag(tag);
        }

        public List<User_Post> GetAllPosts()
        {
            return this.dataAccess.GetAllPosts();
        }

        public List<User_Post> GetAllPostsByTag(int tag_id)
        {
            return this.dataAccess.GetAllPostsByTag(tag_id);
        }

        public List<User_Post> GetAllTagIdsContainingString(string searchString)
        {
            return this.dataAccess.GetAllTagIdsContainingString(searchString);
        }

        public List<Tag> GetAllTagsForPostId(int postId)
        {
            return this.dataAccess.GetAllTagsForPostId(postId);
        }

        public List<User_Post> GetAllFollowedPostByPostId(int postId)
        {
            return this.dataAccess.GetAllFollowedPostByPostId(postId);
        }

    }
}