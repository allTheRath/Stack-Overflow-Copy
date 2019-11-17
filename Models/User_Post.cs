using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QA_Project.Models
{
    public class User_Post
    {
        public int Id { get; set; }
        // unique id of post.

        public DateTime PostedOn { get; set; }

        // a string representing a associative tag of post. 
        public string Title { get; set; }

        // a string representing a post.. either an answer, or a question, or comment.
        public string Discription { get; set; }

        //an int count of votes either upvote or down vote
        public int Voted_Count { get; set; }

        //an int count of answers either of question or of answer.. So we can filter the questions with most answers.
        // or sort the answers with replied or followed answers. Kind of like a thread in slack for conversation.
        public int Answered_Count { get; set; }

        public int? Comment_Count { get; set; }

        public int? View_Count { get; set; }

        // A bool to represent the accepetance of the current reply. this is strictly for the question poster to set it to true.
        public bool Acceptance_Of_Post { get; set; }

        // an enum representing a type of user post.
        public Post_Type Post_Type { get; set; }

        // Virtual props.

        // Navigation of tags.
        public string Associated_User_Id { get; set; }
        public virtual ApplicationUser Poster { get; set; }

        public virtual ICollection<Followed_Post> Followed_Posts { get; set; }
        public virtual ICollection<Post_Tag> Associated_Tags { get; set; }
        public virtual ICollection<User_Vote_Of_Post> User_Vote_Of_Posts { get; set; }
    }

    public enum Post_Type
    {
        Question,
        Answer,
        Comment
    }

    public class Tag
    {
        public int Id { get; set; }
        public string Tag_Name { get; set; }
        public virtual ICollection<Post_Tag> Post_Tags { get; set; }
    }

    public class Post_Tag
    {
        public int Id { get; set; }
        public int TagId { get; set; }
        public virtual Tag Tag { get; set; }
        public int Post_Id { get; set; }
        public virtual User_Post User_Post { get; set; }
    }

    public class Followed_Post
    {
        public int Id { get; set; }
        public int Main_Post_Id { get; set; }
        public virtual User_Post Main_Post { get; set; }
        public int Followed_Post_Id { get; set; }
        public virtual User_Post _Followed_Post { get; set; }
        //an int representing order of post. oder 1 represents first follow up post. a comment is also ordered as continuose number.
        // ex a question 1 with ans 5 which has second comment. current order is a complete order.
        public int Order { get; set; }

    }

    public class User_Vote_Of_Post
    {
        public int Id { get; set; }
        public string Voter_Id { get; set; }
        public Voted_Type Voted_Type { get; set; }
        public int Post_Id { get; set; }
    }

    public enum Voted_Type
    {
        UpVoted,
        DownVoted
    }

    // All required viewmodels here..........
    public class AddTagsViewmodel
    {
        public List<Tag> Tags { get; set; }
        public List<int> SelectedTagIds { get; set; }
        public List<Tag> AlreadySelectedTags { get; set; }
        public int PostId { get; set; }
        public DateTime PostedOn { get; set; }
        public string Title { get; set; }
        public string Discription { get; set; }
        public int Voted_Count { get; set; }
        public int Answered_Count { get; set; }
    }

}

