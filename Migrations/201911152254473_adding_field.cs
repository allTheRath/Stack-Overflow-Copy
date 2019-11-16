namespace QA_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adding_field : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.User_Post", "View_Count", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.User_Post", "View_Count");
        }
    }
}
