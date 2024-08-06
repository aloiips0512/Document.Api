using System;
using Document.Models;
using Document.Repository.Interfaces;
using MongoDB.Driver;

namespace Document.Repository
{
    public class ClientRepository : IClientRepository
    {
        private readonly IMongoCollection<Client> _clients;
        private readonly IMongoCollection<FinancialDocument> _documents;


        public ClientRepository(IMongoDatabase database)
        {
            _clients = database.GetCollection<Client>("Clients");
            _documents = database.GetCollection<FinancialDocument>("Documents");
        }

        public async Task<Client> GetClientByIdAsync(string id)
        {
            return await _clients.Find(client => client.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Client> GetClientByTenantAndDocumentIdAsync(string tenantId, string documentId)
        {
            var documentFilter = Builders<FinancialDocument>.Filter.And(
                        Builders<FinancialDocument>.Filter.Eq(d => d.TenantId, tenantId),
                        Builders<FinancialDocument>.Filter.Eq(d => d.DocumentId, documentId)
                    );

            var document = await _documents.Find(documentFilter).FirstOrDefaultAsync();

            if (document == null)
            {
                return null;
            }

            // find client from clientId from the document
            var clientFilter = Builders<Client>.Filter.And(
                Builders<Client>.Filter.Eq(p => p.TenantId, tenantId),
                Builders<Client>.Filter.Eq(p => p.ClientId, document.ClientId)
            );

            var client = await _clients.Find(clientFilter).FirstOrDefaultAsync();

            return client;
        }

        public async Task<bool> IsClientWhitelistedAsync(string tenantId, string clientId)
        {
            var client = await _clients.Find(c => c.TenantId == tenantId && c.ClientId == clientId).FirstOrDefaultAsync();
            return client != null;
        }

        public async Task CreateClientAsync(Client client)
        {
            await _clients.InsertOneAsync(client);
        }
    }
}

