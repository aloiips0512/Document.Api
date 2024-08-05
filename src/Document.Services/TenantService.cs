using System;
using Document.Models;
using Document.Repository;
using Document.Services.Interfaces;
using MongoDB.Driver;

namespace Document.Services
{
    public class TenantService : ITenantService
    {
        private readonly List<Guid> _whitelistedTenants = new List<Guid>
        {
            Guid.Parse("11111111-1111-1111-1111-111111111111"), // Example tenant IDs
            Guid.Parse("22222222-2222-2222-2222-222222222222")
        };
    

        public Response<bool> IsTenantWhitelistedAsync(Guid tenantId)
        {
            bool isWhitelisted = _whitelistedTenants.Contains(tenantId);
            return isWhitelisted
                ? Response<bool>.CreateSuccessResponse(true)
                : Response<bool>.CreateErrorResponse("Tenant ID is not whitelisted.");
        }
    }
}

