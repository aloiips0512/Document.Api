using System;
namespace Document.Models
{
	public class DocumentRequest
	{
        public string ProductCode { get; set; }
        public string TenantId { get; set; }
        public string DocumentId { get; set; }
    }
}

