using System;
using System.Collections.Generic;
using Antlr.Runtime;
using AutoMapper;
using CodeWarrior.App.ViewModels.Questions;
using CodeWarrior.Model;

namespace CodeWarrior.App.Mappers
{

    public class BindingModelToDatabaseModel : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<QuestionBindingModel, Question>()
                .ForMember(question=>question.PostedOn,
                expr => expr.MapFrom(questionModel => DateTime.UtcNow))

                .ForMember(question => question.Tags,
                    expr => expr.MapFrom(questionModel => questionModel.Tags ?? new string[] {}))

                .ForMember(question => question.Comments,
                    expr => expr.MapFrom(questionModel => new List<Comment>()))

                .ForMember(question => question.Answers,
                    expr => expr.MapFrom(questionModel => new List<Answer>()));
        }
    }
}