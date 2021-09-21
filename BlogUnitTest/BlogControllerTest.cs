using System.Collections;
using System.Collections.Generic;
using Blog.Controllers;
using Blog.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace BlogUnitTest
{
    [TestClass]
    public class BlogControllerTest
    {
        Mock<IBlogRepository> _repository;

        [TestMethod]
        public void IndexReturnsNotNullResult()
        {
            // Arrange
            _repository = new Mock<IBlogRepository>();
            var controller = new BlogController(_repository.Object);
            // Act
            var result = controller.Index() as ViewResult;
            // Assert
            Assert.IsNotNull(result, "View Result is null");
        }

        
        [TestMethod]
        public void IndexReturnsAllProducts()
        {
            List<Blog.Models.Entities.Blog> blog = new List<Blog.Models.Entities.Blog>{
                new Blog.Models.Entities.Blog {BlogId = 1, Name = "First in line", ClosedForPosts = false},
                new Blog.Models.Entities.Blog {BlogId = 2, Name = "Everything was great", ClosedForPosts = false},
            };

            // Arrange
            _repository = new Mock<IBlogRepository>();
            var controller = new BlogController(_repository.Object);
            // Act
            var result = controller.Index() as ViewResult;
            // Assert
            CollectionAssert.AllItemsAreInstancesOfType((ICollection)result.ViewData.Model, typeof(Blog.Models.Entities.Blog));
            Assert.IsNotNull(result, "View Result is null");
            var products = result.ViewData.Model as List<Blog.Models.Entities.Blog>;
            //Assert.AreEqual(5, products.Count, "Got wrong number of products");
        }

        [TestMethod]
        public void SaveIsCalledWhenProductIsCreated()
        { // Arrange
            _repository = new Mock<IBlogRepository>();
            _repository.Setup(x => x.Save(It.IsAny<Blog.Models.Entities.Blog>()));
            var controller = new BlogController(_repository.Object);
            // Act
            var result = controller.Create(new Blog.Models.Entities.Blog());
            // Assert
            _repository.VerifyAll();
            // test på at save er kalt et bestemt antall ganger
            //_repository.Verify(x => x.save(It.IsAny<Product>()), Times.Exactly(1));
        }


    }
}
