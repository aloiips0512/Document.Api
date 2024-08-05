using System;
using Document.Models;

namespace Document.Services.Interfaces
{
    public interface ITenantService
    {
        Response<bool> IsTenantWhitelistedAsync(Guid tenantId);
    }
}

