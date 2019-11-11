using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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

        // A bool to represent the accepetance of the current reply. this is strictly for the question poster to set it to true.
        public bool Acceptance_Of_Post { get; set; }

        // an enum representing a type of user post.
        public Post_Type Post_Type { get; set; }



        // Virtual props.
        
        // Navigation of tags.
        public virtual ICollection<Post_Tag> Associated_Tags { get; set; }
    
        //Navigation of post. like a post of ans to question or a comment to an answer.
        public virtual AssociatedUser_Of_Post AssociatedUser_Of_Post { get; set; }
       
        // a post associated to another post.
        public virtual AssociatedPost_Of_Post AssociatedPost_Of_Post { get; set; }
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
    }

    public class Post_Tag
    {
        public int Id { get; set; }
        public int TagId { get; set; }
        public virtual Tag Tag { get; set; }
        public int Post_Id { get; set; }
        public virtual User_Post User_Post { get; set; }
    }

    public class AssociatedUser_Of_Post
    {
        public int Id { get; set; }
        public string Associated_User_Id { get; set; }
        public virtual ApplicationUser Poster { get; set; }
        public int User_Post_Id { get; set; }
        public virtual User_Post User_Post { get; set; }
        public Voted_Type Voted_Type { get; set; }
    }

    public class AssociatedPost_Of_Post
    {
        public int Id { get; set; }
        public int Question_Id { get; set; }
        public virtual User_Post Question_Posted { get; set; }
        public int Followed_Post_Id { get; set; }
        public virtual User_Post Followed_Post { get; set; }

        //an int representing order of post. oder 1 represents first follow up post. a comment is also ordered as continuose number.
        // ex a question 1 with ans 5 which has second comment. current order is a complete order.
        public int Order { get; set; }
    }

    public enum Voted_Type
    {
        UpVoted,
        DownVoted
    }
    //- A main page which contains a list of recent questions,
    //    each 10 questions appear in together(pagination), then the next page contains the next 10 questions…etc.
    //- Registered users should be able to post a question.

    //- Registered users should be able to answer a question.

    //- Each question has a list of tags, when a user clicks on a tag, 
    //    he should be directed to a page containing all the questions related to this tag.

    //- Each question page starts with the Title of the question, the body (description), the tags, 
    //  (all are under each other in a vertical way, 
    //    please take a look at an example on stackoverflow) then a list of answers, 
    //   Users can also comment on questions and answers.

    //- Users can sort the questions based on: 1-Newest, 2-Most answers 3-Top Questions of the day 
    //    (question with most answers among the questions that were posted in the last 24 hours)

    //- Each question and answer can have a number of votes, where users can either upvote or downvote them, 
    //   users can’t vote on their own questions and answers.

    //- Each user has a reputation that appear next to his name,
    //    each time a user gets an upvote he gets 5 reputations, a downvote remove 5 reputations.

    
    //- Comments and answers should appear in a vertical way under their targeted questions, just like real QA websites.


    //- Create a system to assign badges to users, for example,
    //  a user with 100 reputations will get a golden badge that appears next to his name.
    
    //- The user who asked the question can mark an answer as the accepted answer.


    // All required viewmodels here..........


}