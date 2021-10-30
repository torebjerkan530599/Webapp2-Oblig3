using Blog.Models.Entities;
using Blog.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace Blog.Data
{
    public class BlogDbContext : IdentityDbContext<ApplicationUser>
    {
        public BlogDbContext(DbContextOptions<BlogDbContext> options)
            : base(options)
        {
        }

        public DbSet<Blogg> Blogs { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Tag> Tags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //this.SeedUsers(modelBuilder);

            //modelBuilder.Ignore<Tag>();
            //modelBuilder.Ignore<PostsAndTags>();

            //fluent API...is this necessary when I also have nav. properties configured in entities?
            /*modelBuilder.Entity<Blogg>().ToTable("Blogg")
                .HasMany(p => p.Posts);

            modelBuilder.Entity<Post>().ToTable("Post")
                .HasOne(p => p.Blog)
                .WithMany(b=>b.Posts)
                .HasForeignKey(k=>k.BlogId);

            modelBuilder.Entity<Comment>().ToTable("Comment")
                .HasOne(p => p.Post)
                .WithMany(c => c.Comments)
                .HasForeignKey(k => k.PostId);

            
            modelBuilder.Entity<Tag>().ToTable("Tag")
                .HasMany(p => p.Posts);

            modelBuilder.Entity<Post>().ToTable("Post")
                .HasMany(p => p.Tags);*/

            //modelBuilder.Entity<Blogg>().HasOne(i => i.Owner).WithMany(b=>b.Bloggs);

            //modelBuilder.Entity<ApplicationUser>(u => { u.HasData(user1);});

            var userId_01 = Guid.NewGuid().ToString();
            var hash = new PasswordHasher<ApplicationUser>();

             modelBuilder.Entity<ApplicationUser>().HasData(new {
                    Id = userId_01,
                    PasswordHash = hash.HashPassword(null, "Vm$€sKKm34"),
                    UserName = "Pelle",
                    Email = "parafin@rock.com",
                    EmailConfirmed = true,
                    NormalizedUserName = "PELLE",
                    NormalizedEmail = "PARAFIN@ROCK.COM",
                    LockoutEnabled = false,
                    AccessFailedCount = 10,
                    PhoneNumberConfirmed = true,
                    TwoFactorEnabled = false,
            });

            var userId_02 = Guid.NewGuid().ToString();
            modelBuilder.Entity<ApplicationUser>().HasData(new {

                    Id = userId_02,
                    PasswordHash = hash.HashPassword(null, "k8@fkF3ddk"),
                    UserName = "Harry",
                    Email = "harry@dirtymail.com",
                    EmailConfirmed = true,
                    NormalizedUserName = "HARRY",
                    NormalizedEmail  = "HARRY@DIRTYMAIL.COM",
                    LockoutEnabled = false,
                    AccessFailedCount = 10,
                    PhoneNumberConfirmed = true,
                    TwoFactorEnabled = false,
            });

            modelBuilder.Entity<Blogg>()
                .HasData(new
                    {BlogId = 1, ClosedForPosts = false, Created = DateTime.Now, Name = "Lorem ipsum dolor", OwnerId=userId_01, Modified= DateTime.Now});
            modelBuilder.Entity<Blogg>()
                .HasData(new
                    {BlogId = 2, ClosedForPosts = false, Created = DateTime.Now, Name = "Quisque convallis est", OwnerId = userId_02, Modified = DateTime.Now});
            modelBuilder.Entity<Blogg>()
                .HasData(new
                    {BlogId = 3, ClosedForPosts = false, Created = DateTime.Now, Name = "Interdum et malesuada", OwnerId = userId_01, Modified = DateTime.Now});
            modelBuilder.Entity<Blogg>()
                .HasData(new
                    {BlogId = 4, ClosedForPosts = false, Created = DateTime.Now, Name = "Mauris mi velit", OwnerId = userId_01, Modified = DateTime.Now});

            modelBuilder.Entity<Post>()
                .HasData(new Post
                {
                    PostId = 1,
                    BlogId = 1,
                    Title = "First post",
                    Created = DateTime.Now,
                    Content = "Etiam vulputate massa id ante malesuada " +
                              "elementum. Nulla tellus purus, hendrerit rhoncus " +
                              "justo quis, " +
                              "accumsan ultrices nisi. Integer tristique, ligula in convallis aliquam, " +
                              "massa ligula vehicula odio, in eleifend dolor eros ut nunc",
                    NumberOfComments = 2
                });

            modelBuilder.Entity<Comment>()
                .HasData(new Comment {CommentId = 1, PostId = 1, Created = DateTime.Now, Text = "Is this latin?"});

            modelBuilder.Entity<Comment>()
                .HasData(new Comment
                    {CommentId = 2, PostId = 1, Created = DateTime.Now, Text = "Yes, of course it is"});

            modelBuilder.Entity<Post>()
                .HasData(new Post
                {
                    PostId = 2,
                    BlogId = 2,
                    Title = "Second post",
                    Created = DateTime.Now,
                    Content = "Praesent non massa a nisl euismod efficitur. Ut laoreet nisi " +
                              "vel eleifend laoreet. Curabitur vel orci semper, auctor erat vel, " +
                              "dapibus nunc. Integer eget tortor nunc. Fusce ac euismod nibh. " +
                              "Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia curae",
                    NumberOfComments = 1
                });

            modelBuilder.Entity<Comment>()
                .HasData(new Comment
                    {CommentId = 3, PostId = 2, Created = DateTime.Now, Text = "I really like the blog, but Quisque?"});

        }

        private void SeedUsers(ModelBuilder builder)
        {
            ApplicationUser user = new ApplicationUser()
            {
                Id = "b74ddd14-6340-4840-95c2-db12554843e5",
                UserName = "Admin",
                Email = "admin@gmail.com",
                LockoutEnabled = false,
                PhoneNumber = "1234567890"
            };

            PasswordHasher<ApplicationUser> passwordHasher = new PasswordHasher<ApplicationUser>();
            passwordHasher.HashPassword(user, "Admin*123");

            builder.Entity<ApplicationUser>().HasData(user);
        }
    

        private void SeedRoles(ModelBuilder modelBuilder)
        {
        modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole() { Id = "fab4fac1-c546-41de-aebc-a14da6895711", Name = "Admin", ConcurrencyStamp = "1", NormalizedName = "Admin" },
                new IdentityRole() { Id = "c7b013f0-5201-4317-abd8-c211f91b7330", Name = "HR", ConcurrencyStamp = "2", NormalizedName = "Human Resource" }
            );
        }

        private void SeedUserRoles(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>() { RoleId = "fab4fac1-c546-41de-aebc-a14da6895711", UserId = "b74ddd14-6340-4840-95c2-db12554843e5" }
            );
        }
    }
}

