using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Document.Models
{
	public class Company
	{
        public Guid Id { get; set; }
        public string RegistrationNumber { get; set; }
        public string CompanyType { get; set; }
    }
}

