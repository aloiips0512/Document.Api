using System;
using Document.Models;
using Document.Repository.Interfaces;
using MongoDB.Driver;

namespace Document.Repository
{
    public class ClientRepository : IClientRepository
    {
        private readonly IMongoCollection<Client> _clients;

        public ClientRepository(IMongoDatabase database)
        {
            _clients = database.GetCollection<Client>("Clients");
        }

        public async Task<Client> GetClientByIdAsync(Guid id)
        {
            return await _clients.Find(client => client.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Client> GetClientByTenantAndDocumentIdAsync(string tenantId, string documentId)
        {
            return await _clients.Find(client => client.TenantId == tenantId && client.DocumentId == documentId).FirstOrDefaultAsync();
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

