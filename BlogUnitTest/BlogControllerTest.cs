using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
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
        private List<Blogg> _fakeBloggs;
        private IAuthorizationService _authService;
        private ClaimsPrincipal _user;
        private PostController _postController;
        private BlogController _blogController;
        private CommentController _commentController;


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

            _postController = new PostController(_repository.Object, _authService);
            _blogController = new BlogController(_repository.Object, _authService);
            _commentController = new CommentController(_repository.Object, _authService);

            _fakeBloggs = new List<Blogg>{
                new() {BlogId = 1, Name = "First in line", ClosedForPosts = false},
                new() {BlogId = 2, Name = "Everything was great", ClosedForPosts = false}
            };
        }

        [TestMethod]
        public async Task BlogIndexReturnsNotNullResult()
        {
            // Arrange
            //_blogController= new BlogController(_repository.Object, _authService);

            // Act
            var result = (ViewResult)await _blogController.Index();

            // Assert
            Assert.IsNotNull(result, "View Result is null");
        }

        
        /*[TestMethod]
        public async Task IndexReturnsAllBlogsAndIsOfCorrectType()
        {
            // Arrange
            _repository.Setup(x => x.GetAllBlogs(2)).Returns(_fakeBloggs);
            // Act
            var result =  await _blogController.Index() as ViewResult;
            // Assert
            Assert.IsNotNull(result, "View Result is null");
            CollectionAssert.AllItemsAreInstancesOfType((ICollection)result.ViewData.Model, typeof(Blogg));
            //
            var blogs = result.ViewData.Model as List<Blogg>;
            Assert.AreNotEqual(_fakeBloggs.Count, blogs.Count, "Forskjellig antall blogger");
        }*/
       
        [TestMethod]
        public void SaveIsCalledWhenBlogIsCreated()
        { 
            // Arrange
            _blogController.ControllerContext = MockHelpers.FakeControllerContext(true); //true = is logged in
            _repository.Setup(x => x.SaveBlog(It.IsAny<Blogg>(), It.IsAny<ClaimsPrincipal>()));
            
            // Act
            var result = _blogController.Create(new CreateBloggViewModel());
            // Assert
            _repository.VerifyAll();
            Assert.IsNotNull(result, "Result is null");
            // test p� at save er kalt et bestemt antall ganger
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

            // Act
            var validationContext = new ValidationContext(viewModel, null, null);
            var validationResults = new List<ValidationResult>();
            Validator.TryValidateObject(viewModel, validationContext, validationResults, true);
            foreach (var validationResult in validationResults)
            {
                Debug.Assert(validationResult.ErrorMessage != null, "validationResult.ErrorMessage != null");
                _blogController.ModelState.AddModelError(validationResult.MemberNames.First(),
                    validationResult.ErrorMessage);
            }

            var result = _blogController.Create(viewModel) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(validationResults.Count > 0);
        }

        [TestMethod]
        public void CreateRedirectActionRedirectsToIndexAction() {
            // Arrange

            _blogController.ControllerContext = MockHelpers.FakeControllerContext(false);
            
            var tempData =
                new TempDataDictionary(_blogController.ControllerContext.HttpContext, Mock.Of<ITempDataProvider>());
            _blogController.TempData = tempData;
            var viewModel = new CreateBloggViewModel() {
                Name = "Mine ideer til fornybar energi kilder",
                ClosedForPosts = false
            };

            // Act
            var result = _blogController.Create(viewModel) as RedirectToActionResult;

            // Assert
            Assert.IsNotNull(result, "RedirectToIndex needs to redirect to the Index action");
            Assert.AreEqual("Index", result.ActionName);
        }

        [TestMethod]
        public void CreateReturnsNotNullResult() {
            // Act
            var result = (ViewResult)_blogController.Create();

            // Assert
            Assert.IsNotNull(result, "View Result is null");
        }

        [TestMethod]
        public void CreateShouldShowLoginViewFor_Non_AuthorizedUser()
        {
            // Arrange
            _blogController.ControllerContext = MockHelpers.FakeControllerContext(false);

            // Act
            var result = _blogController.Create() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNull(result.ViewName);

        }

        [TestMethod]
        public void DeltePost_RedirectsToNotAllowed()
        {

            //Arrange
            var owner = new IdentityUser()
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
                    Name = "Navnet p� bloggen",
                    ClosedForPosts = false
                }
            };

            _repository.Setup(x => x.GetPost(fakePost.PostId)).Returns(fakePost);
            //var user = new IdentityUser("testuser");
            //user.Id = "1";
            _postController.ControllerContext = MockHelpers.FakeControllerContext(/*true, user.Id, user.UserName*/);
            var tempData = new TempDataDictionary(
                _postController.ControllerContext.HttpContext,
                Mock.Of<ITempDataProvider>()
            );

            _postController.TempData = tempData;

            var result = _postController.DeletePost(fakePost.PostId).Result as RedirectToActionResult;
            Assert.IsNotNull(result, "Should not be null");
            Assert.AreEqual("NotAllowed", result.ActionName);
        }
        


    }
}
