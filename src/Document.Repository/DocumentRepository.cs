using System;
using Document.Models;
using Document.Repository.Interfaces;
using MongoDB.Driver;

namespace Document.Repository
{
    public class DocumentRepository : IDocumentRepository
    {
        private readonly IMongoCollection<FinancialDocument> _documents;

        public DocumentRepository(IMongoDatabase database)
        {
            _documents = database.GetCollection<FinancialDocument>("Documents");
        }

        public async Task<FinancialDocument> GetDocumentByTenantAndDocumentIdAsync(string tenantId, string documentId)
        {
            return await _documents.Find(d => d.TenantId == tenantId && d.DocumentId == documentId).FirstOrDefaultAsync();
        }

        public async Task CreateDocumentAsync(FinancialDocument document)
        {
            await _documents.InsertOneAsync(document);
        }
    }
}

