using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Document.Models
{
    public class Tenant
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public Guid TenantId { get; set; }
    }
}

