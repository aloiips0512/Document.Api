using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace Document.Models
{
	public class Client
	{
        public Guid Id { get; set; }
        public string ClientId { get; set; }
        public string ClientVAT { get; set; }
        public string TenantId { get; set; }
        public string DocumentId { get; set; }  // added for the repository method
    }
}

