namespace QA_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedUserImageUrl : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "ProfileImageUrl", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "ProfileImageUrl");
        }
    }
}
