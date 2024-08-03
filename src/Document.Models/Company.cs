using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Document.Models
{
	public class Company
	{
        [BsonId]
        public ObjectId Id { get; set; }
        public string ClientVAT { get; set; }
        public string RegistrationNumber { get; set; }
        public string CompanyType { get; set; }
    }
}

