using System;
using System.Collections.Generic;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson.Serialization.Attributes;

namespace CodeWarrior.Model
{
    public class Question
    {
        [BsonElement("_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        
        [BsonRequired]
        public string CreatedBy { get; set; }

        public int TotalViews { get; set; }

        public List<string> Tags { get; set; }

        public int UpVote { get; set; }
        public int DownVote { get; set; }

        public List<Answer> Answers { get; set; }

        public List<Comment> Comments { get; set; }
            
        [BsonRequired]
        public string Title { get; set; }

        [BsonRequired]
        public string Description { get; set; }

        public DateTime PostedOn { get; set; }
    }
}