using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Authorization;
using Microsoft.EntityFrameworkCore;
using Blog.Models.Entities;
using Blog.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Blog.Data
{
    public class BlogDbContext : IdentityDbContext<IdentityUser>
    {
        public BlogDbContext(DbContextOptions<BlogDbContext> options)
            : base(options)
        { }
        public DbSet<Blogg> Blogs { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Tag> Tags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

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

            
            /*modelBuilder.Entity<Tag>().ToTable("Tag")
                .HasMany(p => p.Posts);

            modelBuilder.Entity<Post>().ToTable("Post")
                .HasMany(p => p.Tags);*/


            //modelBuilder.Entity<BloggViewModel>().HasNoKey();

            modelBuilder.Entity<Blogg>()
                .HasData(new Blogg{BlogId = 1, ClosedForPosts = false, Created = DateTime.Now, Name = "Lorem ipsum dolor"});
            modelBuilder.Entity<Blogg>()
                .HasData(new Blogg{BlogId = 2, ClosedForPosts = false, Created = DateTime.Now, Name = "Quisque convallis est"});
            modelBuilder.Entity<Blogg>()
                .HasData(new Blogg{BlogId = 3, ClosedForPosts = false, Created = DateTime.Now, Name = "Interdum et malesuada"});
            modelBuilder.Entity<Blogg>()
                .HasData(new Blogg{BlogId = 4, ClosedForPosts = false, Created = DateTime.Now, Name = "Mauris mi velit"});

            modelBuilder.Entity<Post>()
                .HasData(new Post{PostId = 1, BlogId = 1, Title = "First post",Created = DateTime.Now, 
                     Content = "Etiam vulputate massa id ante malesuada " +
                               "elementum. Nulla tellus purus, hendrerit rhoncus " +
                               "justo quis, " +
                               "accumsan ultrices nisi. Integer tristique, ligula in convallis aliquam, " +
                               "massa ligula vehicula odio, in eleifend dolor eros ut nunc", 
                     NumberOfComments = 2});

            modelBuilder.Entity<Comment>()
                .HasData(new Comment{CommentId = 1 ,PostId = 1, Created = DateTime.Now, Text = "Is this latin?"});

            modelBuilder.Entity<Comment>()
                .HasData(new Comment{CommentId = 2, PostId = 1, Created = DateTime.Now, Text = "Yes, of course it is"});

            modelBuilder.Entity<Post>()
                .HasData(new Post{PostId = 2, BlogId = 2, Title = "Second post",Created = DateTime.Now, 
                    Content = "Praesent non massa a nisl euismod efficitur. Ut laoreet nisi " +
                              "vel eleifend laoreet. Curabitur vel orci semper, auctor erat vel, " +
                              "dapibus nunc. Integer eget tortor nunc. Fusce ac euismod nibh. " +
                              "Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia curae", 
                    NumberOfComments = 1});

            modelBuilder.Entity<Comment>()
                .HasData(new Comment{CommentId = 3, PostId = 2, Created = DateTime.Now, Text = "I really like the blog, but Quisque?"});

        }


        //public DbSet<BloggViewModel> BloggViewModel { get; set; }
    }


    
}
