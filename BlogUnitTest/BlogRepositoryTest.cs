using Blog.Data;
using Blog.Models;
using Blog.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Assert = Xunit.Assert;
using Moq;

namespace BlogUnitTest
{
    public abstract class BlogRepositoryTest
    {
        protected DbContextOptions<BlogDbContext> ContextOptions { get; }

        protected BlogRepositoryTest(DbContextOptions<BlogDbContext> contextOptions)
        {
            ContextOptions = contextOptions;

            Seed();
        }

        private void Seed()
        {
            using var context = new BlogDbContext(ContextOptions);
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            //context.Database.Migrate();
            {

            context.Blogs.AddRange(
                new Blogg {ClosedForPosts = false, Created = DateTime.Now, Name = "Lorem ipsum dolor"},
                new Blogg {ClosedForPosts = false, Created = DateTime.Now, Name = "Quisque convallis est"},
                new Blogg {ClosedForPosts = false, Created = DateTime.Now, Name = "Interdum et malesuada"}
            );

            /*context.Posts.AddRange(
                new Post
                {
                        //PostId = 1,
                        BlogId = 1,
                    Title = "First post",
                    Created = DateTime.Now,
                    Content = "Etiam vulputate massa id ante malesuada " +
                              "elementum. Nulla tellus purus, hendrerit rhoncus " +
                              "justo quis, " +
                              "accumsan ultrices nisi. Integer tristique, ligula in convallis aliquam, " +
                              "massa ligula vehicula odio, in eleifend dolor eros ut nunc"
                },
                new Post
                {
                        //PostId = 2,
                        BlogId = 2,
                    Title = "Second post",
                    Created = DateTime.Now,
                    Content = "Praesent non massa a nisl euismod efficitur. Ut laoreet nisi " +
                              "vel eleifend laoreet. Curabitur vel orci semper, auctor erat vel, " +
                              "dapibus nunc. Integer eget tortor nunc. Fusce ac euismod nibh. " +
                              "Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia curae",
                    NumberOfComments = 1
                }
            );
            context.Comments.AddRange(

                new Comment { CommentId = 1, PostId = 1, Created = DateTime.Now, Text = "Is this latin?" },
                new Comment { CommentId = 2, PostId = 1, Created = DateTime.Now, Text = "Yes, of course it is" },
                new Comment { CommentId = 3, PostId = 2, Created = DateTime.Now, Text = "I really like the blog, but Quisque?" }
            );*/

            context.SaveChanges();
            }

        }

        [Fact]
        public async Task CanGetAllBlogs()
        {
            await using var context = new BlogDbContext(ContextOptions);
            //Arrange
            var mockUserManager = MockHelpers.MockUserManager<ApplicationUser>();
            var repository = new BlogRepository(mockUserManager.Object, context);
            //Act
            var result = await repository.GetAllBlogs();
            //Assert
            Assert.Equal(7, result.Count()); //4 in db, 3 in in-memory db
            var blogs = result as List<Blogg>;
            Assert.Equal("Lorem ipsum dolor", blogs[0].Name);
            Assert.Equal("Quisque convallis est", blogs[1].Name);
            Assert.Equal("Interdum et malesuada", blogs[2].Name);
        }

        [Fact]
        public void CanGetBlog()
        {
            using var context = new BlogDbContext(ContextOptions);
            //Arrange
            var mockUserManager = MockHelpers.MockUserManager<ApplicationUser>();
            var repository = new BlogRepository(mockUserManager.Object, context);
            //Act
            var item = repository.GetBlog(2);
            //Assert
            Assert.Equal("Quisque convallis est", item.Name);
        }
        /*
        [Fact]
        public async Task CanSaveBlog()
        {
            using (var context = new BlogDbContext(ContextOptions))
            {
                //Arrange
                var mockUserManager = MockHelpers.MockUserManager<ApplicationUser>();
                var repository = new BlogRepository(mockUserManager.Object, context);
                var blog = new Blogg {Name = "Another old favorite", Created=DateTime.Now, ClosedForPosts=true};
                //Act
                repository.SaveBlog(blog, mockUserManager.Object.).Wait();
                //Assert
                Assert.NotEqual(0, blog.BlogId);

            }
        }

        public async Task IndexReturnsAllBlogsAndIsOfCorrectType()
        {
            Mock<IBlogRepository> _repository = new Mock<IBlogRepository>();
            // Arrange
            _repository.Setup(x => x.GetAllBlogs()).Returns(Task.FromResult(_fakeBloggs.AsEnumerable()));
            // Act
            var result =  await _blogController.Index() as ViewResult;
            // Assert
            Assert.IsNotNull(result, "View Result is null");
            CollectionAssert.AllItemsAreInstancesOfType((ICollection)result.ViewData.Model, typeof(Blogg));
            //
            var blogs = result.ViewData.Model as List<Blogg>;
            Assert.AreNotEqual(_fakeBloggs.Count, blogs.Count, "Forskjellig antall blogger");
        }
        */
    }
}
