using Blog.Models.Entities;
using Blog.Models.ViewModels;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Blog.Models
{
    public interface IBlogRepository
    {
        Task<IEnumerable<Blogg>> GetAllBlogs();
        Task<IEnumerable<Comment>> GetAllComments(); //Returns all comments. Called from WebApi 
        Task<IEnumerable<Comment>> GetAllCommentsOnPost(int postId); //returns all comments on a post. Called from WebApi
        IEnumerable<Post> GetAllPosts(int blogId);
        IEnumerable<Tag> GetAllTagsForBlog(int BlogId);
        Task<IEnumerable<Tag>> GetAllTags();
        IEnumerable<Post> GetAllPostsInThisBlogWithThisTag(int tagId, int blogId);
        Blogg GetBlog(int blogIdToGet);
        Post GetPost(int? id);
        Tag GetTag(int tagIdToGet);
        Task SaveBlog(Blogg blog, ClaimsPrincipal user);
        PostViewModel GetPostViewModel(int? id);
        Task SavePost(Post post, ClaimsPrincipal user);
        Task<bool> SaveComment(Comment comment);
        Task SaveComment(Comment comment, ClaimsPrincipal user);
        Task UpdatePost(Post post);
        Task DeletePost(Post post);
        CreateBloggViewModel GetCreateBlogViewModel(int? id);
        Task UpdateComment(Comment comment);
        Task DeleteComment(Comment comment);
        public void SetBlogStatus(Blogg blog, bool status);
        public bool? IsActive(Blogg blog);
        Comment GetComment(int commentIdToGet);
        public bool CommentExists(int id);
    }
}
