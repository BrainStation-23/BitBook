using System;
using System.ComponentModel.DataAnnotations;

namespace CodeWarrior.App.ViewModels.Posts
{
    public class PostBindingModel
    {
        public string Id { get; set; }

        [Required]
        public string Message { get; set; }

        public string PostedBy { get; set; }
    }
}