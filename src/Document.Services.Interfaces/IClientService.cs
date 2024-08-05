using System;
using Document.Models;

namespace Document.Services.Interfaces
{
    public interface IClientService
    {
        Task<Response<Client>> GetClientInfoAsync(Guid tenantId, Guid documentId);
        Task<Response<bool>> IsClientWhitelistedAsync(Guid tenantId, Guid clientId);
        Task<Response<Company>> GetCompanyInfoAsync(string clientVAT);
    }
}

