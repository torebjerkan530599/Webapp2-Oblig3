using Blog.Data;
using Blog.Models;
using Blog.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Util
{
    public class Seeder
    {
        protected BlogDbContext Context { get; }

        public Seeder(BlogDbContext contextOptions)
        {
            Context = contextOptions;
            Seed();
        }
        private void Seed()
        {
            Context.Database.EnsureDeleted();
            Context.Database.EnsureCreated();

            //context.Database.Migrate();

            var user1 = new ApplicationUser
            {
                UserName = "Diry Harry",
                Email = "harry@dirtymail.com", //nb: provide e-mail during login...
                EmailConfirmed = true,
                NormalizedUserName = "harry@dirtymail.com",
                LockoutEnabled = false,

            };

            var hash1 = new PasswordHasher<ApplicationUser>();
            var hashedPassword1 = hash1.HashPassword(user1, "Vm$€sKKm34");//...and the password
            user1.PasswordHash = hashedPassword1;

            var user2 = new ApplicationUser
            {
                UserName = "Pelle Parafin",
                Email = "solo@rock.com", //nb: provide e-mail during login...
                EmailConfirmed = true,
                NormalizedUserName = "solo@rock.com",
                LockoutEnabled = false,

            };

            var hash2 = new PasswordHasher<ApplicationUser>();
            var hashedPassword2 = hash2.HashPassword(user1, "k8@fkF3ddk");//...and the password
            user2.PasswordHash = hashedPassword2;



            Context.Blogs.AddRange(
                new Blogg { BlogId = 6, ClosedForPosts = false, Created = DateTime.Now, Name = "Ridderne av det runde bord", Owner = user1 },
                new Blogg { BlogId = 7, ClosedForPosts = true, Created = DateTime.Now, Name = "Veslefrikk med fela", Owner = user1 },
                new Blogg { BlogId = 8, ClosedForPosts = false, Created = DateTime.Now, Name = "Kong salamon og hvitebjørn", Owner = user2 }
            );

            Post post1;
            Post post2;

            Context.Posts.AddRange(
                post1 = new Post
                {
                    PostId = 1,
                    BlogId = 6,
                    Title = "First post",
                    Created = DateTime.Now,
                    Content = "Ridderne av det runde bord er i walisiske sagn kretsen av riddere som samlet seg rundt den britanniske kongen Arthur",
                    Owner = user1
                },
                post2 = new Post
                {
                    PostId = 2,
                    BlogId = 8,
                    Title = "Forklaring på eventyret",
                    Created = DateTime.Now,
                    Content = "Om natten blir Valemon forvandlet til en mann, men forvandles tilbake til bjørn om dagen. Arthur er enig.",
                    Owner = user2
                }
            );
            Context.Comments.AddRange(

                new Comment { CommentId = 1, PostId = 1, Created = DateTime.Now, Text = "Hvor mange riddere?" },
                new Comment { CommentId = 2, PostId = 1, Created = DateTime.Now, Text = "Arthur var sjef" },
                new Comment { CommentId = 3, PostId = 2, Created = DateTime.Now, Text = "Svært overdrevet sjef" }
            );

            //var comments = _db.Comments.Include(c => c.Post).Include(o=>o.Owner).Where(c => c.PostId == postIdToGet);
            //var post = await _db.Posts.Include(c => c.Comments).FirstAsync(x => x.PostId == postIdToGet);
            //var commentList = post.Comments;//.Where(c => c.PostId == postIdToGet);
            //return post.Comments;

            /*var blogs = context.Blogs.Include(o => o.Owner).Where(b => b.BlogId == 1);

            //alle felter merket required (her BlogId) på entiteter må sendes med!
            var post1 = new Post
            {
                Title = "Denne posten tilhører taggene 'suppe' og 'kaffe' ",
                Content = "kobles sammen med to tagger: 'suppe' og 'kaffe')",
                BlogId = 1,
                //Owner = ,//Hent ut eier av Blog med id 1 

            };

            var post2 = new Post
            {
                Title = "Denne posten tilhører kun taggen 'kaffe' ",
                Content = "kobles sammen med 'kaffe' ",
                BlogId = 1
            };*/

            Context.AddRange(
                new Tag
                {
                    TagLabel = "riddere",
                    Created = DateTime.Now,
                    Posts = new List<Post> { post1 }
                },

                new Tag
                {
                    TagLabel = "Arthur",
                    Created = DateTime.Now,
                    Posts = new List<Post> { post1, post2 }
                });

            Context.SaveChanges();
        }
    }
}
