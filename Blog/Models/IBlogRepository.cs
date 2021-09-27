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
        IEnumerable<Blogg> GetAllBlogs();

        Blogg GetBlog(int blogIdToGet);
        Task SaveBlog(Blogg blog,  ClaimsPrincipal user); 

        IEnumerable<Post> GetAllPosts(int blogId);
        PostViewModel GetPostViewModel(int? id);
        Post GetPost(int? id);
        Task SavePost(Post post,  ClaimsPrincipal user);

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
