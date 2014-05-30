using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CodeWarrior.Model
{
    public class Comment
    {
        [BsonElement("_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        
        [BsonRequired]
        public string Description { get; set; }

        [BsonRequired]
        public string CommentedBy { get; set; }

        public DateTime CommentedOn { get; set; }
    }
}