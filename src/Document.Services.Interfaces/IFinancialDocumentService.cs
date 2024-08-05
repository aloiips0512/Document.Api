using System;
using Document.Models;

namespace Document.Services.Interfaces
{
    public interface IFinancialDocumentService
    {
        Task<Response<string>> GetDocumentAsync(Guid tenantId, Guid documentId, string productCode);
        Task<Response<string>> AnonymizeDocument(string documentJson, string productCode);

    }
}

