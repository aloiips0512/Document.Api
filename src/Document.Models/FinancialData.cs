using System;
using MongoDB.Bson.Serialization.Attributes;

namespace Document.Models
{
    public class FinancialData
    {
        [BsonElement("account_number")]
        public string AccountNumber { get; set; }

        [BsonElement("balance")]
        public double Balance { get; set; }

        [BsonElement("currency")]
        public string Currency { get; set; }

        [BsonElement("transactions")]
        public List<Transaction> Transactions { get; set; }
    }
}

