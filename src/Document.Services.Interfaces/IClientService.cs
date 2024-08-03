using System;
using Document.Models;
using Document.Models.DTO;

namespace Document.Services.Interfaces
{
    public interface IClientService
    {
        Task<Response<ClientInfoDto>> GetClientInfoAsync(Guid tenantId, Guid documentId);
        Task<Response<bool>> IsClientWhitelistedAsync(Guid tenantId, Guid clientId);
        Task<Response<CompanyInfoDto>> GetCompanyInfoAsync(string clientVAT);
    }
}

