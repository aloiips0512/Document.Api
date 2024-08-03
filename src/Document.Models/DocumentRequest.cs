using System;
namespace Document.Models
{
	public class DocumentRequest
	{
        public Guid TenantId { get; set; }
        public Guid DocumentId { get; set; }
        public string ProductCode { get; set; }
    }
}

