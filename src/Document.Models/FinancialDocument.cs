using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Document.Models;

public class FinancialDocument
{
    public Guid Id { get; set; }
    public string DocumentId { get; set; }
    public string TenantId { get; set; }
    public string ClientId { get; set; }
    public FinancialData FinancialData { get; set; }
}

