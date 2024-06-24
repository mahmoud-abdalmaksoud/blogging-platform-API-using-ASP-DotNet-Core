using Microsoft.EntityFrameworkCore;
using UdemyProject.Data.Models;

namespace UdemyProject.Data
{
    public class AppDbContext : DbContext
    {

        public DbSet<User> Users { get; set; }
        public DbSet<UserSalary> UserSalaries { get; set; }
        public DbSet<UserJobInfo> UserJobInfos { get; set; }
        public DbSet<Auth>  UsersAuth { get; set; }
        public DbSet<Post> Posts { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public AppDbContext()
        {
                
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .ToTable("Users", schema: "TutorialAppSchema");

            modelBuilder.Entity<UserSalary>()
                .ToTable("UserSalary", schema: "TutorialAppSchema")
                .HasOne<User>()
                .WithOne()
                .HasForeignKey<UserSalary>(us => us.UserId);

            modelBuilder.Entity<UserJobInfo>()
                .ToTable("UserJobInfo", schema: "TutorialAppSchema")
                .HasOne<User>()
                .WithOne()
                .HasForeignKey<UserJobInfo>(uj => uj.UserId);

            modelBuilder.Entity<Auth>().ToTable("Auth", schema: "TutorialAppSchema");

            modelBuilder.Entity<Post>()
               .ToTable("Posts", schema: "TutorialAppSchema")
               .HasOne(p => p.user)
               .WithMany(u => u.Posts)
               .HasForeignKey(p => p.UserId);

        }
    }

}