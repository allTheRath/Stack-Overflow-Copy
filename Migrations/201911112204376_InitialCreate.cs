namespace QA_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.User_Post",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PostedOn = c.DateTime(nullable: false),
                        Title = c.String(),
                        Discription = c.String(),
                        Voted_Count = c.Int(nullable: false),
                        Answered_Count = c.Int(nullable: false),
                        Acceptance_Of_Post = c.Boolean(nullable: false),
                        Post_Type = c.Int(nullable: false),
                        Associated_User_Id = c.String(),
                        Poster_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Poster_Id)
                .Index(t => t.Poster_Id);
            
            CreateTable(
                "dbo.Post_Tag",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TagId = c.Int(nullable: false),
                        Post_Id = c.Int(nullable: false),
                        User_Post_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Tags", t => t.TagId, cascadeDelete: true)
                .ForeignKey("dbo.User_Post", t => t.User_Post_Id)
                .Index(t => t.TagId)
                .Index(t => t.User_Post_Id);
            
            CreateTable(
                "dbo.Tags",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Tag_Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Followed_Post",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Main_Post_Id = c.Int(nullable: false),
                        Followed_Post_Id = c.Int(nullable: false),
                        Order = c.Int(nullable: false),
                        _Followed_Post_Id = c.Int(),
                        Main_Post_Id1 = c.Int(),
                        User_Post_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User_Post", t => t._Followed_Post_Id)
                .ForeignKey("dbo.User_Post", t => t.Main_Post_Id1)
                .ForeignKey("dbo.User_Post", t => t.User_Post_Id)
                .Index(t => t._Followed_Post_Id)
                .Index(t => t.Main_Post_Id1)
                .Index(t => t.User_Post_Id);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        DisplayName = c.String(),
                        Reputation = c.Int(nullable: false),
                        User_Badge = c.Int(nullable: false),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.User_Vote_Of_Post",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Voter_Id = c.String(),
                        Voted_Type = c.Int(nullable: false),
                        Post_Id = c.Int(nullable: false),
                        User_Post_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User_Post", t => t.User_Post_Id)
                .Index(t => t.User_Post_Id);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.User_Vote_Of_Post", "User_Post_Id", "dbo.User_Post");
            DropForeignKey("dbo.User_Post", "Poster_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Followed_Post", "User_Post_Id", "dbo.User_Post");
            DropForeignKey("dbo.Followed_Post", "Main_Post_Id1", "dbo.User_Post");
            DropForeignKey("dbo.Followed_Post", "_Followed_Post_Id", "dbo.User_Post");
            DropForeignKey("dbo.Post_Tag", "User_Post_Id", "dbo.User_Post");
            DropForeignKey("dbo.Post_Tag", "TagId", "dbo.Tags");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.User_Vote_Of_Post", new[] { "User_Post_Id" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Followed_Post", new[] { "User_Post_Id" });
            DropIndex("dbo.Followed_Post", new[] { "Main_Post_Id1" });
            DropIndex("dbo.Followed_Post", new[] { "_Followed_Post_Id" });
            DropIndex("dbo.Post_Tag", new[] { "User_Post_Id" });
            DropIndex("dbo.Post_Tag", new[] { "TagId" });
            DropIndex("dbo.User_Post", new[] { "Poster_Id" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.User_Vote_Of_Post");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Followed_Post");
            DropTable("dbo.Tags");
            DropTable("dbo.Post_Tag");
            DropTable("dbo.User_Post");
        }
    }
}
