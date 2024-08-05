using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Document.Models
{
    public class Tenant
    {
        public Guid Id { get; set; }
        public string TenantId { get; set; }
        public string Name { get; set; }
        public bool Whitelisted { get; set; }
    }
}

