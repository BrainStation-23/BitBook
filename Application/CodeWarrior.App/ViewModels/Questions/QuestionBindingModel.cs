using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CodeWarrior.Model;

namespace CodeWarrior.App.ViewModels.Questions
{
    public class QuestionBindingModel
    {
        [Required]
        public string CreatedBy { get; set; }

        public int TotalViews { get; set; }

        public string[] Tags { get; set; }

        public int UpVote { get; set; }
        public int DownVote { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }
    }
}