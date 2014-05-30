﻿using MongoDB.Bson;
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
        public string PosteddBy { get; set; }

        [BsonIgnore]
        public int LikeCount
        {
            get { return LikedBy.Count; }
        }

        public List<string> LikedBy { get; set; }

        public List<Comment> Comments { get; set; }
    }
}
