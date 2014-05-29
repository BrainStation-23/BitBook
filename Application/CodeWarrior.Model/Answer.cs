using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CodeWarrior.Model
{
    public class Answer
    {
        [BsonElement("_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public bool IsAccepted { get; set; }
       
        [BsonRequired]
        public string Description { get; set; }
        
        [BsonRequired]
        public string AnsweredBy { get; set; }

        public int UpVote { get; set; }
        public int DownVote { get; set; }

        public List<Comment> Comments { get; set; }

        public DateTime AnsweredOn { get; set; }
    }
}