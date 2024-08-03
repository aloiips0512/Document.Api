using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace Document.Models
{
	public class Client
	{
        [BsonId]
        public ObjectId Id { get; set; }
        public Guid TenantId { get; set; }
        public Guid ClientId { get; set; }
        public string ClientVAT { get; set; }
    }
}

