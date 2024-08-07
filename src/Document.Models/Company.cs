using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Document.Models
{
	public class Company
	{
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string RegistrationNumber { get; set; }
        public string CompanyType { get; set; }
        public string ClientId { get; set; }
    }
}

