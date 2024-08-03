using System;
using Document.Models;

namespace Document.Services.Interfaces
{
    public interface ITenantService
    {
        Task<Response<bool>> IsTenantWhitelistedAsync(Guid tenantId);
    }
}

