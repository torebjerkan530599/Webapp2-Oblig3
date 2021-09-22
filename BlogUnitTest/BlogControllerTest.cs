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
            List<Blog.Models.Entities.Blogg> blog = new List<Blog.Models.Entities.Blogg>{
                new Blog.Models.Entities.Blogg {BlogId = 1, Name = "First in line", ClosedForPosts = false},
                new Blog.Models.Entities.Blogg {BlogId = 2, Name = "Everything was great", ClosedForPosts = false},
            };

            // Arrange
            _repository = new Mock<IBlogRepository>();
            var controller = new BlogController(_repository.Object);
            // Act
            var result = controller.Index() as ViewResult;
            // Assert
            CollectionAssert.AllItemsAreInstancesOfType((ICollection)result.ViewData.Model, typeof(Blog.Models.Entities.Blogg));
            Assert.IsNotNull(result, "View Result is null");
            var products = result.ViewData.Model as List<Blog.Models.Entities.Blogg>;
            //Assert.AreEqual(5, products.Count, "Got wrong number of products");
        }

        [TestMethod]
        public void SaveIsCalledWhenProductIsCreated()
        { // Arrange
            _repository = new Mock<IBlogRepository>();
            _repository.Setup(x => x.Save(It.IsAny<Blog.Models.Entities.Blogg>()));
            var controller = new BlogController(_repository.Object);
            // Act
            var result = controller.Create(new Blog.Models.Entities.Blogg());
            // Assert
            _repository.VerifyAll();
            // test på at save er kalt et bestemt antall ganger
            //_repository.Verify(x => x.save(It.IsAny<Product>()), Times.Exactly(1));
        }


    }
}
