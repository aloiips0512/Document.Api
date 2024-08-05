using System;
using Document.Models;

namespace Document.Repository.Interfaces
{
    public interface IDocumentRepository
    {
        Task<FinancialDocument> GetDocumentByTenantAndDocumentIdAsync(string tenantId, string documentId);
        Task CreateDocumentAsync(FinancialDocument document);
    }

}

