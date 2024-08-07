using System;
using Document.Models;

namespace Document.Services.Interfaces
{
    public interface IFinancialDocumentService
    {
        Task<Response<string>> GetFinancialDocumentAsync(string tenantId, string documentId, string productCode);
    }
}

