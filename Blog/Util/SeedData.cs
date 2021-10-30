using Blog.Data;
using Blog.Models;
using Blog.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Blog.Util
{
    public class SeedData
    {
        protected BlogDbContext Context { get; }

        public SeedData(BlogDbContext contextOptions)
        {
            Context = contextOptions;
            Seed();
        }

        public static void Initialize(IServiceProvider app)
        {
            //using var context = new BlogDbContext(
            //    app.GetRequiredService<DbContextOptions<BlogDbContext>>());
            //context.Database.Migrate();

            //context.Database.EnsureDeleted();
            //context.Database.EnsureCreated();


            //// Get a logger
            //var logger = app.GetRequiredService<ILogger<SeedData>>();

            //// Look for any products.
            //if (context.Blogs.Any())
            //{
            //    logger.LogInformation("The database was already seeded");
            //    return;   // DB has been seeded
            //}


            //context.SaveChanges();
        }
        
        //Seed direkte via BlogRepository
        public void Seed()
        {

            //kode som kan være kjekk å ha
            //var comments = _db.Comments.Include(c => c.Post).Include(o=>o.Owner).Where(c => c.PostId == postIdToGet);
            //var post = await _db.Posts.Include(c => c.Comments).FirstAsync(x => x.PostId == postIdToGet);
            //var commentList = post.Comments;//.Where(c => c.PostId == postIdToGet);
            //var blogs = context.Blogs.Include(o => o.Owner).Where(b => b.BlogId == 1);
            
            Post post3;
            Post post4;
            Post post5;
            Context.Posts.AddRange(
                post3 = new Post
                {
                    BlogId = 1,
                    Title = "Lengenden om Arthur",
                    Created = DateTime.Now,
                    Content = "Ridderne av det runde bord er i walisiske sagn kretsen av riddere som samlet seg rundt den britanniske kongen Arthur (#riddere, #Arthur)",
                },
                post4 = new Post
                {
                    BlogId = 1,
                    Title = "Tid for eventyr",
                    Created = DateTime.Now,
                    Content = "Om natten blir Valemon forvandlet til en mann, men forvandles tilbake til bjørn om dagen. Arthur er enig (#eventyr, #Arthur)",
                },
                
                post5 = new Post
                {
                    BlogId = 1,
                    Title = "Tid for eventyr",
                    Created = DateTime.Now,
                    Content = "Ingenting av det som står i eventyrene er sant (#eventyr).",
                }
            );

            Context.AddRange(
                new Tag
                {
                    TagLabel = "#riddere",
                    Created = DateTime.Now,
                    Posts = new List<Post> { post3 }
                },

                new Tag
                {
                    TagLabel = "#eventyr",
                    Created = DateTime.Now,
                    Posts = new List<Post> { post4, post5 }
                },

                new Tag
                {
                    TagLabel = "#Arthur",
                    Created = DateTime.Now,
                    Posts = new List<Post> { post3, post4 }
                });

            Context.SaveChanges();
        }
    }
}
