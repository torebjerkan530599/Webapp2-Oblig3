using Blog.Authorization;
using Blog.Controllers;
using Blog.Models;
using Blog.Models.Entities;
using Blog.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BlogUnitTest
{

    [TestClass]
    public class BlogControllerTest
    {
        private Mock<IBlogRepository> _repository;
        private Mock<UserManager<ApplicationUser>> _mockUserManager;
        private List<Blogg> _fakeBloggs;
        private Blogg _fakeBlog;
        private List<Post> _fakePosts;
        private List<Tag> _fakeTags;
        private List<Comment> _fakeComments;
        private IndexViewModel _fakeIndexViewModel;
        private BloggViewModel _fakeBlogViewModel;
        private PostViewModel _fakePostViewModel;
        private IAuthorizationService _authService;
        private ApplicationUser _appUser; 
        private BlogApplicationUser _blogApplicationUser; 
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
            _mockUserManager = MockHelpers.MockUserManager<ApplicationUser>();
            _mockUserManager.Setup(x => x.GetUserId(It.IsAny<ClaimsPrincipal>())).Returns("1234"); //mock userid
            var authHandler = new BlogOwnerAuthorizationHandler(_mockUserManager.Object);

            _authService = BuildAuthorizationService(services =>
            {
                services.AddScoped(_ => _repository?.Object);
                services.AddScoped<IAuthorizationHandler>(_ => authHandler);

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
                new() {BlogId = 2, Name = "Everything was great", ClosedForPosts = false},
                new() {BlogId = 3, Name = "Nothing new to see", ClosedForPosts = false}
            };

            _fakeBlog = new Blogg
            {
                BlogId = 1, Name = "Løs kanon", ClosedForPosts = false
            };


            _fakePosts = new List<Post>
            {
                new() {PostId = 1, BlogId = 1, Title = "Reaksjonsevne som en polsk vareheis"},
                new() {PostId = 2, BlogId = 1, Title = "Drama is life with the dull bits cut out"}, //-A. Hitchcock
                new() {PostId = 3, BlogId = 1, Title = "Det var en gang et menneske med god reaksjonsevne"}
            };

            _fakeComments = new List<Comment>
            {
                new() {CommentId = 1, PostId = 1, Text = "Det blir litt som å sitte på glattcelle å høre på værmeldingen"},
                new() {CommentId = 2, PostId = 1, Text = "Det er 10 år der ingenting skjer og der er uker der alt skjer"},
                new() {CommentId = 3, PostId = 1, Text = "Gjett Ohm, eller skulle jeg sagt det i Watt?"},
            };

            _fakeTags = new List<Tag>
            {
                new() {TagId = 1, TagLabel = "Reaksjonsevne"},
                new() {TagId = 1, TagLabel = "dull"},
                new() {TagId = 1, TagLabel = "Vare"},
            };

            _fakeIndexViewModel = new IndexViewModel()
            {
                Blogs = _fakeBloggs,
                Posts = _fakePosts,
                Tags = _fakeTags,
                Comments = _fakeComments
            };

            _fakeBlogViewModel = new BloggViewModel()
            {
                BlogId = 2,
                Name = "I Bloggens navn",
                Posts = _fakePosts
            };


            _fakePostViewModel = new PostViewModel()
            {
                PostId = 1,
                Title = "En viktig tittel",
                BlogId = 1,
                Tags = _fakeTags,
                Comments = _fakeComments
            };

            _appUser = new ApplicationUser()
            {
                UserName = "Rekkehus",
                Password = "Dw7+SW#8@trEk",
            };

            _blogApplicationUser = new BlogApplicationUser()
            {
                Owner  = _appUser,
                Blog   = _fakeBlog,
                BlogId = _fakeBlog.BlogId
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
        public void CreateRedirectActionRedirectsToIndexAction()
        {
            // Arrange

            _blogController.ControllerContext = MockHelpers.FakeControllerContext(false);

            var tempData =
                new TempDataDictionary(_blogController.ControllerContext.HttpContext, Mock.Of<ITempDataProvider>());
            _blogController.TempData = tempData;
            var viewModel = new CreateBloggViewModel()
            {
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
        public void CreateReturnsNotNullResult()
        {
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
            var owner = new ApplicationUser()
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
            _postController.ControllerContext = MockHelpers.FakeControllerContext();
            var tempData = new TempDataDictionary(
                _postController.ControllerContext.HttpContext,
                Mock.Of<ITempDataProvider>()
            );

            _postController.TempData = tempData;

            var result = _postController.DeletePost(fakePost.PostId).Result as RedirectToActionResult;
            Assert.IsNotNull(result, "Should not be null");
            Assert.AreEqual("NotAllowed", result.ActionName);
        }

        [TestMethod]
        public void ReadBlogReturnsBloggViewModel()
        {
            // Arrange
            _repository.Setup(x => x.GetBlog(2)).Returns(_fakeBlog);

            // Act
            var result = (ViewResult) _blogController.ReadBlog(1,2);
            var bloggViewModell = result.ViewData.Model as BloggViewModel;

            // Assert
            Assert.IsNotNull(result, "View Result is null");
            Assert.AreEqual(_fakeBlogViewModel.GetType(), bloggViewModell.GetType(), "Samme viewmodell");
        }

        [TestMethod]
        public void ReadPostReturnsPostViewModel()
        {
            // Arrange
            _postController.ControllerContext = MockHelpers.FakeControllerContext(false);

            var tempData = new TempDataDictionary(_postController.ControllerContext.HttpContext,
                Mock.Of<ITempDataProvider>());
            _postController.TempData = tempData;

            _repository.Setup(x => x.GetPostViewModel(1)).Returns(_fakePostViewModel);

            // Act
            var result = (ViewResult) _postController.ReadPost(1);
            var readComments = result.ViewData.Model as PostViewModel;

            // Assert
            Assert.IsNotNull(result, "View Result is null");
            Assert.AreEqual(readComments.Tags.Count, _fakePostViewModel.Tags.Count, "Samme viewmodell, med samme antall tag");
        }

        [TestMethod]
        public void SubscribeReturnsRedirectAction()
        {
            // Arrange
            _blogController.ControllerContext = MockHelpers.FakeControllerContext(false);
            var tempData = new TempDataDictionary(_blogController.ControllerContext.HttpContext,
                Mock.Of<ITempDataProvider>());
            _blogController.TempData = tempData;

            _repository.Setup(x => x.GetPostViewModel(1)).Returns(_fakePostViewModel);
            _repository.Setup(x => x.GetBlog(3)).Returns(_fakeBlog);
            _repository.Setup(x => x.GetOwner(It.IsAny<ClaimsPrincipal>())).Returns(Task.FromResult(new ApplicationUser { Id = "92b38b02-04e3-4529-85b6-fb1b13e9ca74" }));

            // Act
            var result = _blogController.SubscribeToBlog(3) as RedirectToActionResult;

            // Assert
            Assert.IsNotNull(result, "RedirectToIndex needs to redirect to the Index action");
            Assert.AreEqual("Index", result.ActionName);
            Assert.AreEqual("Blog", result.ControllerName);
        }

        [TestMethod]
        public void UnSubscribeToBlogReturnsRedirectAction()
        {
            // Arrange
            _blogController.ControllerContext = MockHelpers.FakeControllerContext(false);
            var tempData = new TempDataDictionary(_blogController.ControllerContext.HttpContext,
                Mock.Of<ITempDataProvider>());
            _blogController.TempData = tempData;

            _repository.Setup(x => x.GetBlogApplicationUser(_fakeBlog, _appUser)).Returns(_blogApplicationUser);
            _repository.Setup(x => x.GetBlog(1)).Returns(_fakeBlog);
            _repository.Setup(x => x.GetOwner(It.IsAny<ClaimsPrincipal>())).Returns(Task.FromResult(new ApplicationUser { Id = "92b38b02-04e3-4529-85b6-fb1b13e9ca74" }));
            _mockUserManager.Setup(x => x.GetUserId(It.IsAny<ClaimsPrincipal>())).Returns("1234");

            // Act
            var result = _blogController.UnSubscribeToBlog(1) as RedirectToActionResult;

            // Assert
            Assert.IsNotNull(result, "RedirectToIndex needs to redirect to the Index action");
            Assert.AreEqual("Index", result.ActionName);
            Assert.AreEqual("Blog", result.ControllerName);
        }

    }
}
