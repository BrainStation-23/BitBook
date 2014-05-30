using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CodeWarrior.App.ViewModels.Posts
{
    public class CommentBindingModel
    {
        public string Id { get; set; }

        [Required]
        public string PostId { get; set; }

        [Required]
        public string Description { get; set; }

        public string CommentedBy { get; set; }

        public DateTime CommentedOn { get; set; }
     }
}