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

        IEnumerable<Post> GetAllPosts(int blogId);

        //CreateBloggViewModel GetCreateBlogViewModel();

        CreateBloggViewModel GetCreateBlogViewModel(int? id);

        Task SaveBlog(Blogg blog,  ClaimsPrincipal user); 

        Task SavePost(Post post,  ClaimsPrincipal user); 

        Task SaveComment(Comment newComment, ClaimsPrincipal user);

        PostViewModel GetPostViewModel(int? id);

        public void SetBlogStatus(Blogg blog, bool status);

        public bool? IsActive(Blogg blog);

    }
}
