using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.Data;
using Blog.Models;
using Blog.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using BlogUnitTest;
using Microsoft.AspNetCore.Mvc;

namespace BlogUnitTest
{
    [TestClass]
    public class BlogRepositoryTest
    {

        protected DbContextOptions<BlogDbContext> ContextOptions { get; }

        protected BlogRepositoryTest(DbContextOptions<BlogDbContext> contextOptions)
        {
            ContextOptions = contextOptions;
            
            Seed();
        }

        private void Seed()
        {
            using (var context = new BlogDbContext(ContextOptions))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                //context.Database.Migrate();

                

                var user = new IdentityUser
                {
                    UserName = "Diry Harry",
                    Email = "harry@dirtymail.com", //nb: provide e-mail during login...
                    EmailConfirmed = true,
                    NormalizedUserName = "harry@dirtymail.com",
                    LockoutEnabled = false,

                };

                var hash = new PasswordHasher<IdentityUser>();
                var hashedPassword = hash.HashPassword(user, "YourPassword");//...and the password
                user.PasswordHash = hashedPassword;

                context.Blogs.AddRange(
                    new Blogg { BlogId = 1, ClosedForPosts = false, Created = DateTime.Now, Name = "Lorem ipsum dolor", Owner = user },
                    new Blogg{BlogId = 2, ClosedForPosts = false, Created = DateTime.Now, Name = "Quisque convallis est"},
                    new Blogg{BlogId = 3, ClosedForPosts = false, Created = DateTime.Now, Name = "Interdum et malesuada" }
                );

                context.Posts.AddRange(
                    new Post{PostId = 1, BlogId = 1, Title = "First post",Created = DateTime.Now, 
                        Content = "Etiam vulputate massa id ante malesuada " +
                                  "elementum. Nulla tellus purus, hendrerit rhoncus " +
                                  "justo quis, " +
                                  "accumsan ultrices nisi. Integer tristique, ligula in convallis aliquam, " +
                                  "massa ligula vehicula odio, in eleifend dolor eros ut nunc"  },
                    new Post{PostId = 2, BlogId = 2, Title = "Second post",Created = DateTime.Now, 
                        Content = "Praesent non massa a nisl euismod efficitur. Ut laoreet nisi " +
                                  "vel eleifend laoreet. Curabitur vel orci semper, auctor erat vel, " +
                                  "dapibus nunc. Integer eget tortor nunc. Fusce ac euismod nibh. " +
                                  "Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia curae", 
                        NumberOfComments = 1 }
                );
                context.Comments.AddRange(

                    new Comment{CommentId = 1 ,PostId = 1, Created = DateTime.Now, Text = "Is this latin?"},
                    new Comment{CommentId = 2, PostId = 1, Created = DateTime.Now, Text = "Yes, of course it is"},
                    new Comment{CommentId = 3, PostId = 2, Created = DateTime.Now, Text = "I really like the blog, but Quisque?"}
                );

                context.SaveChanges();
            }
            
        }

        [TestMethod]
        public async Task CanGetAllBlogs()//does test have to be syncronous?
        {
            await using var context = new BlogDbContext(ContextOptions);
            //Arrange
            var mockUserManager = MockHelpers.MockUserManager<IdentityUser>();
            var repository = new BlogRepository(mockUserManager.Object, context);
            //Act
            var result = await repository.GetAllBlogs();
            //Assert
            Assert.AreEqual(3, result.Count());
            //var blogs = result as List<Blogg>;
            //Assert.AreEqual("Lorem ipsum dolor", blogs[0].Name);
            //Assert.AreEqual("Quisque convallis est", blogs[1].Name);
            //Assert.AreEqual("Interdum et malesuada", blogs[2].Name);
        }

        /*[TestMethod]
        public async Task IndexReturnsAllBlogsAndIsOfCorrectType()
        {
            // Arrange
            _repository.Setup(x => x.GetAllBlogs()).Returns(_fakeBloggs);
            // Act
            var result =  await _blogController.Index() as ViewResult;
            // Assert
            Assert.IsNotNull(result, "View Result is null");
            CollectionAssert.AllItemsAreInstancesOfType((ICollection)result.ViewData.Model, typeof(Blogg));
            //
            var blogs = result.ViewData.Model as List<Blogg>;
            Assert.AreNotEqual(_fakeBloggs.Count, blogs.Count, "Forskjellig antall blogger");
        }*/

    }
}
