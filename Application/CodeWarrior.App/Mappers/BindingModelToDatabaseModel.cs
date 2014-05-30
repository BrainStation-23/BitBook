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
            Mapper.CreateMap<Question, Question>();

            Mapper.CreateMap<QuestionBindingModel, Question>()
                .ForMember(question => question.PostedOn,
                    expr => expr.MapFrom(questionModel => DateTime.UtcNow))

                .ForMember(question => question.Tags,
                    expr => expr.MapFrom(questionModel => questionModel.Tags ?? new string[] {}))

                .ForMember(question => question.Comments,
                    expr => expr.MapFrom(questionModel => new List<Comment>()))

                .ForMember(question => question.Answers,
                    expr => expr.MapFrom(questionModel => new List<Answer>()));

            Mapper.CreateMap<PostBindingModel, Post>()
                .ForMember(post => post.PostedOn,
                    expr => expr.MapFrom(postModel => DateTime.UtcNow))

                .ForMember(post => post.Likes,
                    expr => expr.MapFrom(postModel => new List<ApplicationUser>()))

                .ForMember(post => post.Comments,
                    expr => expr.MapFrom(postModel => new List<Comment>()));

            Mapper.CreateMap<CommentBindingModel, Comment>()
                .ForMember(comment => comment.CommentedOn,
                    expr => expr.MapFrom(commentModel => DateTime.UtcNow))
                .ForMember(comment => comment.Id,
                    expr => expr.MapFrom(commentModel => ObjectId.GenerateNewId().ToString()));
        }
    }
}