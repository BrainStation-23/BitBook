using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using CodeWarrior.App.ViewModels.Posts;
using CodeWarrior.Model;

namespace CodeWarrior.App.Mappers
{
    public class DatabaseModelToViewModel : Profile
    {
        protected override void Configure()
        {
            //Mapper.CreateMap<ApplicationUser, ApplicationUserViewModel>();
            Mapper.CreateMap<Post, PostViewModel>();
            Mapper.CreateMap<Comment, CommentViewModel>();
        }
    }
}
