using IdentityTest13.Controllers;
using IdentityTest13.Models;
using IdentityTest13.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections;
using System.Collections.Generic;

namespace ProductUnitTest
{
    [TestClass]
    public class ProductControllerTest
    {
        Mock<IProductRepository> _repository;

        List<Product> fakeproducts;
        List<Category> fakecategories;

        [TestInitialize]
        public void SetupContext()
        {

            _repository = new Mock<IProductRepository>();

            fakeproducts = new List<Product>{
                  new Product {Name="Hammer", Price=121.50m, Description="Verktøy"},
                  new Product {Name="Vinkelsliper", Price=1520.00m, Description="Verktøy"},
                  new Product {Name="Melk", Price=14.50m, Description="Dagligvarer"},
                  new Product {Name="Kjøttkaker", Price=32.00m, Description="Dagligvarer"},
                  new Product {Name="Brød", Price=25.50m, Description="Dagligvarer"}
                 };
            fakecategories = new List<Category>{
                  new Category {name="Verktøy", CategoryId=3 },
                  new Category {name="Dagligvarer", CategoryId=2 },
                  new Category {name="Kjøretøy", CategoryId=1}
                };

        }
        [TestMethod]

        public void CreateRedirectActionRedirectsToIndexAction()

        {

            //Arrange
            var mockRepo = new Mock<IProductRepository>();
            var controller = new ProductController(mockRepo.Object);
            controller.ControllerContext = MockHelpers.FakeControllerContext(false);
            var tempData = new TempDataDictionary(controller.ControllerContext.HttpContext, Mock.Of<ITempDataProvider>());
            controller.TempData = tempData;
            var model = new ProductEditViewModel();
            model.Price = 100;
            model.Description = "Description of product";

            //Act
            var result = controller.Create(model) as RedirectToActionResult;

            //Assert
            Assert.IsNotNull(result, "RedirectToIndex needs to redirect to the Index action");
            Assert.AreEqual("Index", result.ActionName as String);

        }
        [TestMethod]
        public void IndexReturnsAllProducts()
        {
            // Arrange
            _repository.Setup(x => x.getAll()).Returns(fakeproducts);
            var controller = new ProductController(_repository.Object);

            // Act
            var result = (ViewResult)controller.Index();

            // Assert
            CollectionAssert.AllItemsAreInstancesOfType((ICollection)result.ViewData.Model, typeof(Product));
            Assert.IsNotNull(result, "View Result is null");
            var products = result.ViewData.Model as List<Product>;
            Assert.AreEqual(5, products.Count, "Got wrong number of products");


        }
        [TestMethod]
        public void SaveIsCalledWhenProductIsCreated()
        {
            // Arrange
            _repository.Setup(x => x.save(It.IsAny<Product>()));
            var controller = new ProductController(_repository.Object);

            // Act
            var result = controller.Create(new ProductEditViewModel());

            // Assert
            _repository.VerifyAll();
            // test på at save er kalt et bestemt antall ganger
            //_repository.Verify(x => x.save(It.IsAny<Product>()), Times.Exactly(1));


        }
        [TestMethod]
        public void DeleteGetsPostAndReturnsAConfirmationView()
        {
            //Arrange	
            Product product = new Product { Name = "TestHammer", Price = 121.50m, Description = "Verktøy" };
            _repository.Setup(x => x.get(1)).Returns(product);
            var controller = new ProductController(_repository.Object);

            //Act
            var result = (ViewResult)controller.Delete(1);
            var productReturn = result.ViewData.Model as Product;

            //Assert
            Assert.AreEqual(product, productReturn, "Got wrong post back");
        }

        [TestMethod]
        public void DeleteCalledWithNoArgumentsReturnsANotFoundResult()
        {
            //Arrange
            var controller = new ProductController(_repository.Object);

            //Act
            var Result = controller.Delete(null);

            // Assert
            Assert.IsInstanceOfType(Result, typeof(NotFoundResult));

        }

        [TestMethod]
        public void DeleteCalledWithWrongProductIdReturnsANotFoundResult()
        {
            //Arrange
            _repository.Setup(x => x.get(42)).Returns<Product>(null);
            var controller = new ProductController(_repository.Object);

            //Act
            var Result = controller.Delete(42);

            // Assert
            Assert.IsInstanceOfType(Result, typeof(NotFoundResult));

        }

        [TestMethod]
        public void DeleteConfirmedCallsDeleteInIRepository()
        {
            //Arrange
            Product product = new Product { Name = "TestHammer", Price = 121.50m, Description = "Verktøy" };
            _repository.Setup(x => x.get(1)).Returns(product);
            var controller = new ProductController(_repository.Object);

            //Act
            controller.DeleteConfirmed(1);

            // Assert
            _repository.Verify(x => x.remove(product));

        }
    }
}
