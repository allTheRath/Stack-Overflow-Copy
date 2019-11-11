using QA_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace QA_Project.Controllers
{
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


    public class HomeController : Controller
    {
        // Moc design implementation.
        Application_Business_Logic Application_Business_Logic = new Application_Business_Logic(new Application_DataAccess());

        public ActionResult Index(string searchString, string currentFilter, int? page)
        {
            var questions = new List<User_Post>();
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            if (!String.IsNullOrEmpty(searchString))
            {
                // return posts by tag name.. search name..

            }
            else
            {
                questions.AddRange(Application_Business_Logic.GetAllPosts());
            }

            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(questions.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult AllTags(int? post_id)
        {
            List<Tag> AllTags = new List<Tag>();
            if (post_id == null)
            {
                int postid = Convert.ToInt32(post_id);
                Application_Business_Logic.GetAllTagsForPostId(postid);
            }
            return View(AllTags);
        }

        public ActionResult AllFollowedPost(int? post_id)
        {
            List<User_Post> All_Followed_Posts = new List<User_Post>();
            if(post_id != null)
            {
                int postid = Convert.ToInt32(post_id);
                All_Followed_Posts.AddRange(Application_Business_Logic.GetAllFollowedPostByPostId(postid));
            }

            return View(All_Followed_Posts);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}