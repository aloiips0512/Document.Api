using System;
using Document.Models;
using Document.Repository;
using Document.Services.Interfaces;
using MongoDB.Driver;

namespace Document.Services
{
    public class TenantService : ITenantService
    {
        private readonly AppDbContext _context;

        public TenantService(AppDbContext context)
        {
            _context = context;
        }

        public Task<Response<bool>> IsTenantWhitelistedAsync(Guid tenantId)
        {
            throw new NotImplementedException();
        }
    }
}

