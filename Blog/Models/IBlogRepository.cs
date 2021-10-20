using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Blog.Models.Entities;
using Blog.Models.ViewModels;

namespace Blog.Models
{
    public interface IBlogRepository
    {
        Task<IEnumerable<Blogg>> GetAllBlogs();
        Task<IEnumerable<Comment>> GetAllComments(); //Returns all comments. Called from WebApi 

        Task<IEnumerable<Comment>> GetAllCommentsOnPost(int postId); //returns all comments on a post. Called from WebApi
        //IEnumerable<Comment> GetAllComments(); //Called from WebApi 
        IEnumerable<Post> GetAllPosts(int blogId);

        Blogg GetBlog(int blogIdToGet);
        Task SaveBlog(Blogg blog,  ClaimsPrincipal user); 

        PostViewModel GetPostViewModel(int? id);
        Post GetPost(int? id);
        Task SavePost(Post post,  ClaimsPrincipal user);

        Task SaveComment(Comment comment);

        Task UpdatePost(Post post);
        Task DeletePost(Post post);
        CreateBloggViewModel GetCreateBlogViewModel(int? id);

        Task SaveComment(Comment newComment, ClaimsPrincipal user);

        Task UpdateComment(Comment comment);
        Task DeleteComment(Comment comment);


        public void SetBlogStatus(Blogg blog, bool status);

        public bool? IsActive(Blogg blog);
        Comment GetComment(int commentIdToGet);
    }
}
