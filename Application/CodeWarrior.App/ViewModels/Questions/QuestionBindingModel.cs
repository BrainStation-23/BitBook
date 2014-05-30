using System.ComponentModel.DataAnnotations;

namespace CodeWarrior.App.ViewModels.Questions
{
    public class QuestionBindingModel
    {
        public string Id { get; set; }

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