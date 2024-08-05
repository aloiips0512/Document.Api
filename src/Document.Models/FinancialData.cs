using System;

namespace Document.Models
{
    public class FinancialData
    {
        public string AccountNumber { get; set; }
        public double Balance { get; set; }
        public string Currency { get; set; }
        public List<Transaction> Transactions { get; set; }
    }
}

