using AutoMapper;
using Blog.Models.Entities;
using Blog.Models.ViewModels;

namespace Blog.Util
{
    public class BlogProfiles : Profile
    {
        public BlogProfiles()
        {
            CreateMap<Post, PostViewModel>();
        }


    }
}
