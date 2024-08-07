using System;
using Document.Models;

namespace Document.Services.Interfaces
{
    public interface IClientService
    {
        Task<Response<Client>> GetClientByTenantAndDocumentIdAsync(string tenantId, string documentId);

        Task<Response<bool>> IsClientWhitelistedAsync(string tenantId, string clientId);
    }
}

