using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using CodeWarrior.App.ViewModels.Account;
using CodeWarrior.App.ViewModels.Posts;
using CodeWarrior.Model;

namespace CodeWarrior.App.Mappers
{
    public class DatabaseModelToViewModel : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<ApplicationUser, ApplicationUserViewModel>()
                .ForMember(viewModel => viewModel.AvatarUrl,
                    expr => expr.MapFrom(userModel => userModel.AvatarUrl ?? "/Content/Images/noimage.png"));

            //Mapper.CreateMap<Post, PostViewModel>()
            //    .ForMember(viewModel => viewModel.LikedBy,
            //        expr => expr.MapFrom(userModel => new List<ApplicationUserViewModel>()))
            //        .ForMember(viewModel => viewModel.Comments,
            //        expr => expr.MapFrom(userModel => new List<CommentViewModel>())); ;

            Mapper.CreateMap<Comment, CommentViewModel>();
        }
    }
}
