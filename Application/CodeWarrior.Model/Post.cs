using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace CodeWarrior.Model
{
    public class Post
    {
        [BsonElement("_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonRequired]
        public string Message { get; set; }

        public DateTime PostedOn { get; set; }

        [BsonRequired]
        public string PostedBy { get; set; }

        [BsonIgnore]
        public int LikeCount
        {
            get { return LikedBy.Count; }
        }

        private List<string> _likedBy;

        public List<string> LikedBy
        {
            get { return _likedBy ?? (_likedBy = new List<string>()); }
            set { _likedBy = value; }
        }

        public List<Comment> Comments { get; set; }
    }
}
