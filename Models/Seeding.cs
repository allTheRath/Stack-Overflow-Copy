using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace QA_Project.Models
{
    public class Seeding
    {
        Random random = new Random();

        public void UpdateQuestionCount(ApplicationDbContext db)
        {
            var allquestins = db.All_Posts.Where(x => x.Post_Type == Post_Type.Question).ToList();
            foreach (var q in allquestins)
            {
                q.View_Count = (new Random()).Next(5, 500);
                var allfollowedPostC = db.Followed_Posts.Where(x => x.Main_Post_Id == q.Id).ToList().Count();

                q.Answered_Count = allfollowedPostC;
            }
            db.SaveChanges();

        }

        public void SeedUserAccounts(UserManager<ApplicationUser> userManager, ApplicationDbContext db)
        {

            for (int i = 0; i < 200; i++)
            {
                ApplicationUser user = new ApplicationUser { UserName = "Jay" + i + "@test.com", Email = "Jay" + i + "@test.com" };
                userManager.Create(user, "Jay12345!");
            }

        }

        public void SeedDatabaseWithData(ApplicationDbContext db)
        {
            List<Post_Tag> AllPostedTags = new List<Post_Tag>();
            List<User_Post> Questions = new List<User_Post>();
            List<User_Post> Answers = new List<User_Post>();
            List<User_Post> Comments = new List<User_Post>();
            List<Tag> AllTags = new List<Tag>();
            var AllUsers = db.Users.ToList();

            var allAta = db.All_Tags.ToList();
            foreach (var a in allAta)
            {
                db.All_Tags.Remove(a);
                db.SaveChanges();
            }

            List<string> titleNames = new List<string>();
            // create tags..
            for (int i = 0; i < 50; i++)
            {
                Tag t = new Tag();
                string titleOrTag = GetTitle();
                t.Tag_Name = titleOrTag;
                titleNames.Add(titleOrTag + " " + GetTitle());

                db.All_Tags.Add(t);
                db.SaveChanges();


                Tag tagRetrived = db.All_Tags.Where(x => x.Tag_Name == t.Tag_Name).FirstOrDefault();
                AllTags.Add(tagRetrived);

            }

            for (int i = 0; i < titleNames.Count(); i++)
            {
                // create a question.
                User_Post question = new User_Post();
                question.Title = titleNames[i];
                question.Discription = GetDiscription();
                question.Voted_Count = GetVotedCount();
                question.Post_Type = Post_Type.Question;
                question.PostedOn = DateTime.Now.AddDays(-random.Next(1, 5)).AddMilliseconds(random.Next(10000));
                question.Acceptance_Of_Post = false;
                question.Answered_Count = 0;

                // a random user selection from all users list.
                int indexOfUser = random.Next(200);
                question.Associated_User_Id = AllUsers[indexOfUser].Id;
                question.Poster = AllUsers[indexOfUser];

                db.All_Posts.Add(question);
                db.SaveChanges();
                User_Post addedQuestion = db.All_Posts.ToList().Where(x => x == question).FirstOrDefault();
                Questions.Add(addedQuestion);
                //Keeping track of questions.

                var allSimilarPosts = AllTags.Where(x => question.Title.Contains(x.Tag_Name));
                foreach (var tt in allSimilarPosts)
                {
                    Post_Tag post_Tag = new Post_Tag();
                    post_Tag.TagId = tt.Id;
                    post_Tag.Tag = tt;
                    post_Tag.Post_Id = addedQuestion.Id;
                    post_Tag.User_Post = addedQuestion;

                    db.Tag_Of_Post.Add(post_Tag);
                    db.SaveChanges();

                }

                int amountOfComments = random.Next(1, 5);

                for (int k = 1; k < 6; k++)
                {

                    User_Post answer = new User_Post();
                    answer.Title = titleNames[i] + "Answer :(";
                    answer.Discription = GetDiscription();
                    answer.Voted_Count = GetVotedCount();
                    answer.Post_Type = Post_Type.Answer;
                    answer.PostedOn = DateTime.Now.AddDays(-random.Next(4)).AddMilliseconds(-random.Next(1000));
                    answer.Acceptance_Of_Post = AcceptanceOfPost();
                    answer.Answered_Count = 0;

                    // a random user selection from all users list.
                    int indexOfUserAns = random.Next(200);
                    answer.Associated_User_Id = AllUsers[indexOfUserAns].Id;
                    answer.Poster = AllUsers[indexOfUserAns];

                    db.All_Posts.Add(answer);
                    db.SaveChanges();
                    User_Post addedAnswer = db.All_Posts.ToList().Where(x => x == answer).FirstOrDefault();
                    Answers.Add(addedAnswer);

                    Followed_Post followed_Posted_Answer = new Followed_Post();
                    followed_Posted_Answer.Followed_Post_Id = addedAnswer.Id;
                    followed_Posted_Answer.Main_Post_Id = question.Id;
                    followed_Posted_Answer.Main_Post = question;
                    followed_Posted_Answer._Followed_Post = addedAnswer;
                    followed_Posted_Answer.Order = k;

                    db.Followed_Posts.Add(followed_Posted_Answer);
                    db.SaveChanges();

                    for (int c = 1; c < amountOfComments; c++)
                    {
                        User_Post comment = new User_Post();
                        comment.Title = "Comment :(";
                        comment.Discription = GetDiscription();
                        comment.Voted_Count = 0;
                        comment.Post_Type = Post_Type.Comment;
                        comment.PostedOn = DateTime.Now.AddDays(-random.Next(4)).AddMilliseconds(-random.Next(10000));
                        comment.Acceptance_Of_Post = false;
                        comment.Answered_Count = 0;

                        // a random user selection from all users list.
                        int indexOfUserComment = random.Next(200);
                        comment.Associated_User_Id = AllUsers[indexOfUserComment].Id;
                        comment.Poster = AllUsers[indexOfUserComment];

                        db.All_Posts.Add(comment);
                        db.SaveChanges();
                        User_Post addedComment = db.All_Posts.ToList().Where(x => x == comment).FirstOrDefault();
                        Comments.Add(addedComment);

                        Followed_Post followed_Posted_Comment = new Followed_Post();
                        followed_Posted_Answer.Followed_Post_Id = addedComment.Id;
                        followed_Posted_Answer.Main_Post_Id = addedAnswer.Id;
                        followed_Posted_Answer.Main_Post = addedAnswer;
                        followed_Posted_Answer._Followed_Post = addedComment;
                        followed_Posted_Answer.Order = c;

                        db.Followed_Posts.Add(followed_Posted_Comment);
                        db.SaveChanges();

                    }
                    // add few comments..
                }

            }

            //// add few votes..
            //for (int o = 1; o < 5000; o++)
            //{

            //}

        }


        public string GetTitle()
        {
            string title = "";
            int lengthRandom = random.Next(4, 10);
            int space = random.Next(4, lengthRandom);
            for (int i = 0; i < lengthRandom; i++)
            {
                int charactor = random.Next(65, 91);
                if (i > space && i % space == 0)
                {
                    title += " ";
                }
                title += Convert.ToChar(charactor);
            }

            return title;
        }

        public string GetDiscription()
        {
            string discription = "";
            int lengthRandom = random.Next(24, 150);
            int space = random.Next(4, 8);
            for (int i = 0; i < lengthRandom; i++)
            {
                int charactor = random.Next(65, 91);
                if (i > space && i % space == 0)
                {
                    discription += " ";
                }
                discription += Convert.ToChar(charactor);
            }

            return discription;
        }

        public int GetVotedCount()
        {
            return random.Next(10, 200);
        }

        public bool AcceptanceOfPost()
        {
            if (random.Next(2) == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}