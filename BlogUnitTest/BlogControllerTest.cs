using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using Blog.Authorization;
using Blog.Controllers;
using Blog.Data;
using Blog.Models;
using Blog.Models.Entities;
using Blog.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using ProductUnitTest;

namespace BlogUnitTest
{
    
    [TestClass]
    public class BlogControllerTest
    {
        private Mock<IBlogRepository> _repository;
        Mock<UserManager<IdentityUser>> _mockUserManager;
        private List<Blogg> _blogs;
        private IAuthorizationService _authService;


        //https://codingblast.com/asp-net-core-unit-testing-authorizationservice-inside-controller/*
        private IAuthorizationService BuildAuthorizationService(Action<IServiceCollection> setupServices = null)
        {
            var services = new ServiceCollection();
            services.AddAuthorization();
            services.AddLogging();
            services.AddOptions();
            setupServices?.Invoke(services);
            return services.BuildServiceProvider().GetRequiredService<IAuthorizationService>();
        }

        [TestInitialize]
        public void SetupContext()
        {
            _repository = new Mock<IBlogRepository>();
            _mockUserManager = MockHelpers.MockUserManager<IdentityUser>();
            _authService = BuildAuthorizationService(services =>
            {

                services.AddScoped(_ => _repository?.Object);
                services.AddScoped<IAuthorizationHandler, BlogOwnerAuthorizationHandler>();

                services.AddAuthorization(options =>
                {
                    //*
                    //options.AddPolicy("PolicyName", policy => policy.Requirements.Add(new MyCustomRequirement()));
                    //Feiler på new MyCustomRequirement() fordi jeg ikke har den.

                    options.AddPolicy("Basic", policy =>
                    {
                        policy.AddAuthenticationSchemes("Basic");
                        policy.RequireClaim("Permission", "CanViewPage");
                    });
                }); 

                

            });

            
     
     


            _blogs = new List<Blogg>{
                new() {BlogId = 1, Name = "First in line", ClosedForPosts = false},
                new() {BlogId = 2, Name = "Everything was great", ClosedForPosts = false}
            };
        }
        
        [TestMethod]
        public void BlogIndexReturnsNotNullResult()
        {
            // Arrange
            //var mockBlogOwnerAuthHandler = new BlogOwnerAuthorizationHandler(_mockUserManager.Object);
            var controller = new BlogController(_repository.Object, _authService);
            // Act
            var result = controller.Index() as ViewResult;
            // Assert
            Assert.IsNotNull(result, "View Result is null");
        }

        
        [TestMethod]
        public void BlogIndexReturnsAllBlogs()
        {
            // Arrange
            _repository.Setup(x => x.GetAllBlogs()).Returns(_blogs);
            var controller = new BlogController(_repository.Object,  _authService);
            // Act
            var result = controller.Index() as ViewResult;
            // Assert
            CollectionAssert.AllItemsAreInstancesOfType((ICollection)result.ViewData.Model, typeof(Blogg));
            Assert.IsNotNull(result, "View Result is null");
            var products = result.ViewData.Model as List<Blogg>;
            //Assert.AreEqual(5, products.Count, "Got wrong number of products");
        }

        [TestMethod]
        public void SaveIsCalledWhenBlogIsCreated()
        { 
            // Arrange
          
            var controller = new BlogController(_repository.Object, _authService);
            controller.ControllerContext = MockHelpers.FakeControllerContext(true); //true = is logged in
            _repository.Setup(x => x.SaveBlog(It.IsAny<Blogg>(), It.IsAny<ClaimsPrincipal>()));
            
            // Act
            var result = controller.Create(new CreateBloggViewModel());
            // Assert
            _repository.VerifyAll();
            Assert.IsNotNull(result, "Result is null");
            // test på at save er kalt et bestemt antall ganger
            _repository.Verify(x => x.SaveBlog(It.IsAny<Blogg>(), It.IsAny<ClaimsPrincipal>()), Times.Exactly(1));
        }

        [TestMethod]
        public void CreateViewIsReturnedWhenInputIsNotValid() 
        {
            // Arrange
            var viewModel = new CreateBloggViewModel() 
            {
                Name = ""
            };
            var controller = new BlogController(_repository.Object,  _authService);

            // Act
            var validationContext = new ValidationContext(viewModel, null, null);
            var validationResults = new List<ValidationResult>();
            Validator.TryValidateObject(viewModel, validationContext, validationResults, true);
            foreach (var validationResult in validationResults)
                controller.ModelState.AddModelError(validationResult.MemberNames.First(),
                    validationResult.ErrorMessage);

            var result = controller.Create(viewModel) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(validationResults.Count > 0);
        }

        [TestMethod]
        public void CreateRedirectActionRedirectsToIndexAction() {
            // Arrange
            var controller = new BlogController(_repository.Object,  _authService) {
                ControllerContext = MockHelpers.FakeControllerContext(false)
            };
            var tempData =
                new TempDataDictionary(controller.ControllerContext.HttpContext, Mock.Of<ITempDataProvider>());
            controller.TempData = tempData;
            var viewModel = new CreateBloggViewModel() {
                Name = "Mine ideer til fornybar energi kilder",
                ClosedForPosts = false
            };

            // Act
            var result = controller.Create(viewModel) as RedirectToActionResult;

            // Assert
            Assert.IsNotNull(result, "RedirectToIndex needs to redirect to the Index action");
            Assert.AreEqual("Index", result.ActionName);
        }

        [TestMethod]
        public void CreateReturnsNotNullResult() {
            // Arrange
            var controller = new BlogController(_repository.Object,  _authService);

            // Act
            var result = (ViewResult)controller.Create();

            // Assert
            Assert.IsNotNull(result, "View Result is null");
        }

        [TestMethod]
        public void CreateShouldShowLoginViewFor_Non_AuthorizedUser()
        {
            // Arrange
            var controller = new BlogController(_repository.Object,  _authService); 
            controller.ControllerContext = MockHelpers.FakeControllerContext(false);

            // Act
            var result = controller.Create() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNull(result.ViewName);

        }
        


    }
}
