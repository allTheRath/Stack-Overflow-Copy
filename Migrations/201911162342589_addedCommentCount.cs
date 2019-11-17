namespace QA_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedCommentCount : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.User_Post", "Comment_Count", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.User_Post", "Comment_Count");
        }
    }
}
