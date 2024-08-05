using System;
namespace Document.Models
{
    public class Transaction
    {
        public string TransactionId { get; set; }
        public double Amount { get; set; }
        public string Date { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
    }
}

