using System;
using Document.Models;
using Document.Repository;
using Document.Repository.Interfaces;
using Document.Services.Interfaces;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;

namespace Document.Services
{
    public class TenantService : ITenantService
    {
        private readonly ITenantRepository _tenantRepository;
        private readonly ILogger<TenantService> _logger;

        public TenantService(ITenantRepository tenantRepository, ILogger<TenantService> logger)
        {
            _tenantRepository = tenantRepository;
            _logger = logger;
        }

        public async Task<Response<bool>> IsTenantWhitelistedAsync(string tenantId)
        {
            try
            {
                bool isWhitelisted = await _tenantRepository.IsTenantWhitelistedAsync(tenantId);
                return isWhitelisted
                    ? Response<bool>.CreateSuccessResponse(true)
                    : Response<bool>.CreateWarningResponse("Tenant not whitelisted.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving the financial document for TenantId: {TenantId}", tenantId);
                return Response<bool>.CreateErrorResponse("An error occurred while retrieving the financial document.");
            }

        }
    }
}

