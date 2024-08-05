using Document.Models;

namespace Document.Repository.Interfaces;

public interface IClientRepository
{
    Task<Client> GetClientByIdAsync(Guid id);
    Task<Client> GetClientByTenantAndDocumentIdAsync(string tenantId, string documentId);
    Task<bool> IsClientWhitelistedAsync(string tenantId, string clientId);
    Task CreateClientAsync(Client client);
}

