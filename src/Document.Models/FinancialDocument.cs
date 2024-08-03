using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Document.Models;

public class FinancialDocument
{
    [BsonId]
    public Guid Id { get; set; }
    public Guid TenantId { get; set; }
    public Guid DocumentId { get; set; }
    public string Data { get; set; }
    public Guid ClientId { get; set; }
    public string ClientVAT { get; set; }
}

