using System;
namespace Document.Models.DTO
{
	public class FinancialDocumentDto
	{
        public Guid TenantId { get; set; }
        public Guid DocumentId { get; set; }
        public string Data { get; set; }
        public Guid ClientId { get; set; }
        public string ClientVAT { get; set; }
    }
}

