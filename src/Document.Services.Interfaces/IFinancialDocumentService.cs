using System;
using Document.Models;
using Document.Models.DTO;

namespace Document.Services.Interfaces
{
    public interface IFinancialDocumentService
    {
        Task<Response<string>> GetDocumentAsync(Guid tenantId, Guid documentId);
        Task<string> AnonymizeDocument(string documentJson, string productCode);

    }
}

