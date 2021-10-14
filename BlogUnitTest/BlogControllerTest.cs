using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
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
using BlogUnitTest;

namespace BlogUnitTest
{
    
    [TestClass]
    public class BlogControllerTest
    {
        private Mock<IBlogRepository> _repository;
        private Mock<UserManager<IdentityUser>> _mockUserManager;
        private List<Blogg> _blogs;
        private IAuthorizationService _authService;
        private ClaimsPrincipal _user;
        private PostController postController;


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
            _mockUserManager.Setup(x => x.GetUserId(It.IsAny<ClaimsPrincipal>())).Returns("1234"); //mock userid
            var authHandler = new BlogOwnerAuthorizationHandler(_mockUserManager.Object);

            _authService = BuildAuthorizationService(services =>
            {
                services.AddScoped(_ => _repository?.Object);
                services.AddScoped<IAuthorizationHandler>(_=> authHandler);

                services.AddAuthorization(options =>
                {
                    //options.AddPolicy("PolicyName", policy => policy.Requirements.Add(new MyCustomRequirement()));

                    options.AddPolicy("Basic", policy =>
                    {
                        policy.AddAuthenticationSchemes("Basic");
                        policy.RequireClaim("Permission", "CanViewPage");
                    });
                }); 

                

            });

            postController = new PostController(_repository.Object, _authService);

            _blogs = new List<Blogg>{
                new() {BlogId = 1, Name = "First in line", ClosedForPosts = false},
                new() {BlogId = 2, Name = "Everything was great", ClosedForPosts = false}
            };
        }

        [TestMethod]
        public void DeltePost_RedirectsToNotAllowed()
        {

            //Arrange
            var owner = new IdentityUser("test")
            {
                Id = "12345"
            };
            var fakePost = new Post
            {
                BlogId = 3,
                PostId = 6,
                Title = "Dette er en tittel",
                Content = "Dette er litt innhold",
                Owner = owner,
                Blog = new Blogg()
                {
                    Name = "Navnet på bloggen",
                    ClosedForPosts = false
                }
            };

            _repository.Setup(x => x.GetPost(fakePost.PostId)).Returns(fakePost);
            //var user = new IdentityUser("testuser");
            //user.Id = "1";
            postController.ControllerContext = MockHelpers.FakeControllerContext(/*true, user.Id, user.UserName*/);
            TempDataDictionary tempData = new TempDataDictionary(
                postController.ControllerContext.HttpContext,
                Mock.Of<ITempDataProvider>()
            );

            postController.TempData = tempData;

            var result = postController.DeletePost(fakePost.PostId).Result as RedirectToActionResult;
            Assert.IsNotNull(result, "Should not be null");
            Assert.AreEqual("NotAllowed", result.ActionName);
        }


           [TestMethod]
        public async Task BlogIndexReturnsNotNullResult()
        {
            // Arrange
            var blogController= new BlogController(_repository.Object, _authService);

            // Act
            var result = (ViewResult)await blogController.Index();

            // Assert
            Assert.IsNotNull(result, "View Result is null");
        }

        
        [TestMethod]
        public async Task BlogIndexReturnsAllBlogs()
        {
            // Arrange
            _repository.Setup(x => x.GetAllBlogs()).Returns(_blogs);
            var controller = new BlogController(_repository.Object,  _authService);
            // Act
            var result = await controller.Index() as ViewResult;
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
