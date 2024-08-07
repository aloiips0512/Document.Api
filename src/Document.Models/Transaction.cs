using System;
using MongoDB.Bson.Serialization.Attributes;

namespace Document.Models
{
    public class Transaction
    {
        [BsonElement("transaction_id")]
        public string TransactionId { get; set; }
        [BsonElement("amount")]
        public double Amount { get; set; }
        [BsonElement("date")]
        public string Date { get; set; }
        [BsonElement("description")]
        public string Description { get; set; }
        [BsonElement("category")]
        public string Category { get; set; }
    }
}

