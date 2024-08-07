using System;
using Document.Models;

namespace Document.Repository.Interfaces
{
    public interface ITenantRepository
    {
        Task<bool> IsTenantWhitelistedAsync(string tenantId);
    }
}

