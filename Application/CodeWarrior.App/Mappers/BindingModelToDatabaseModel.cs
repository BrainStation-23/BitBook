using System;
using System.Collections.Generic;
using AutoMapper;
using CodeWarrior.App.ViewModels.Posts;
using CodeWarrior.App.ViewModels.Questions;
using CodeWarrior.Model;
using MongoDB.Bson;

namespace CodeWarrior.App.Mappers
{

    public class BindingModelToDatabaseModel : Profile
    {
        protected override void Configure()
        {
            //Mapper.CreateMap<PostBindingModel, Post>()
            //    .ForMember(post => post.LikeCount,
            //        expr => expr.Ignore())
            //    .ForMember(post => post.PostedOn,
            //        expr => expr.MapFrom(postModel => DateTime.UtcNow))

            //    .ForMember(post => post.LikedBy,
            //        expr => expr.MapFrom(postModel => new List<string>()))

            //    .ForMember(post => post.Comments,
            //        expr => expr.MapFrom(postModel => new List<Comment>()));

            /*    
            Mapper.CreateMap<CommentBindingModel, Comment>()
                .ForMember(comment => comment.CommentedOn,
                    expr => expr.MapFrom(commentModel => DateTime.UtcNow))
                .ForMember(comment => comment.Id,
                    expr => expr.MapFrom(commentModel => ObjectId.GenerateNewId().ToString()));
             */ 
        }
    }
}