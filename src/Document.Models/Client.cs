using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace Document.Models
{
	public class Client
	{
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string ClientId { get; set; }
        public string ClientVAT { get; set; }
        public string TenantId { get; set; }
    }
}

