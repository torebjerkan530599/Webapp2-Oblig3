using Blog.Models.Entities;
using Blog.Models.ViewModels;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Blog.Models
{
    public interface IBlogRepository
    {
        //Get: Blogg
        Task<IEnumerable<Blogg>> GetAllBlogs();
        Blogg GetBlog(int blogIdToGet);

        //Get: Viewmodel Blogg
        CreateBloggViewModel GetCreateBlogViewModel(int? id);

        //Get: Viewmodel Innlegg
        PostViewModel GetPostViewModel(int? id);

        //Get: Innlegg
        IEnumerable<Post> GetAllPosts(int blogId);
        Post GetPost(int? id);

        //Get: kommentarer
        Task<IEnumerable<Comment>> GetAllComments(); //Returns all comments. Called from WebApi 

        Task<IEnumerable<Comment>>
            GetAllCommentsOnPost(int postId); //returns all comments on a post. Called from WebApi

        Comment GetComment(int commentIdToGet);

        //Get: Tags
        Task<IEnumerable<Tag>> GetAllTags();
        IEnumerable<Tag> GetAllTagsForBlog(int BlogId);
        IEnumerable<Post> GetAllPostsInThisBlogWithThisTag(int tagId, int blogId);
        Tag GetTag(int tagIdToGet);

        //Save
        Task SaveBlog(Blogg blog, ClaimsPrincipal user);
        Task SavePost(Post post, ClaimsPrincipal user);
        Task SaveComment(Comment comment, ClaimsPrincipal user);
        Task<bool> SaveComment(Comment comment);

        //Update
        Task UpdatePost(Post post);
        Task UpdateComment(Comment comment);

        //Delete
        Task DeletePost(Post post);
        Task DeleteComment(Comment comment);

        //Hjelpemetoder
        void SetBlogStatus(Blogg blog, bool status);
        bool? IsActive(Blogg blog);

        bool CommentExists(int id);

        Task<ApplicationUser> GetOwner(ClaimsPrincipal user);

        Task Subscribe(BlogApplicationUser userSubscriber1);

        BlogApplicationUser GetBlogApplicationUser(Blogg blogToSubscribe, ApplicationUser userSubscriber);

        Task UnSubscribe(BlogApplicationUser userSubscriber1);
        IEnumerable<Blogg> GetLatestUpdatesOnBlog();
        IEnumerable<Blogg> GetAllSubscribedBlogs(ApplicationUser userSubscriber);

        IEnumerable<Post> GetLatestUpdatesOnPosts();
        IEnumerable<Post> GetAllPostsInBlog(int blogIdToGet);

        public void SeedTagsOnPosts();
    }
}
