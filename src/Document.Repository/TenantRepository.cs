using System;
using Document.Models;
using MongoDB.Driver;

namespace Document.Repository.Interfaces
{
    public class TenantRepository : ITenantRepository
    {
        private readonly IMongoCollection<Tenant> _tenants;

        public TenantRepository(IMongoDatabase database)
        {
            _tenants = database.GetCollection<Tenant>("Tenants");
        }

        public async Task<bool> IsTenantWhitelistedAsync(string tenantId)
        {
            var tenant = await _tenants.Find(t => t.TenantId == tenantId).FirstOrDefaultAsync();
            return tenant != null && tenant.Whitelisted;
        }
    }
}

