using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace QA_Project.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public string DisplayName { get; set; }

        public int Reputation { get; set; }

        public Badge_Type User_Badge { get; set; }

        public string ProfileImageUrl { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public enum Badge_Type
    {
        Rookee,
        Browne,
        Silver,
        Golden,
    }

    
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        // All posts ..
        public DbSet<User_Post> All_Posts { get; set; }

        //All followed post ids
        public DbSet<Followed_Post> Followed_Posts { get; set; }

        // All tags ..
        public DbSet<Tag> All_Tags { get; set; }
        
        // Each tag association with each post.
        public DbSet<Post_Tag> Tag_Of_Post { get; set; }

        // Each user for each Posts what is voted?.
        public DbSet<User_Vote_Of_Post> User_Vote_Of_Posts { get; set; }
    
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}