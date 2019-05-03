using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PostPita.Models;

namespace PostPita.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }

        public DbSet<Post> Posts { get; set; }
        public DbSet<Applicant> Applicants { get; set; }

        public DbSet<PostModified> PostsModified { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<CompanyUser> CompanyUsers { get; set; }
        public DbSet<ApplicantUser> ApplicantUser { get; set; }

        public DbSet<Notification> Notifications { get; set; }


    }
}
