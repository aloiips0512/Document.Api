using System;
using System.Linq;
using AutoMapper;
using Document.Models;
using Document.Repository;
using Document.Repository.Interfaces;
using Document.Services.Interfaces;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;

namespace Document.Services
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _clientRepository;
        private readonly ILogger<ClientService> _logger;

        public ClientService(IClientRepository clientRepository, ILogger<ClientService> logger)
        {
            _clientRepository = clientRepository;
            _logger = logger;
        }

        public async Task<Response<Client>> GetClientByTenantAndDocumentIdAsync(string tenantId, string documentId)
        {
            try
            {
                var client = await _clientRepository.GetClientByTenantAndDocumentIdAsync(tenantId, documentId);
                return client != null
                    ? Response<Client>.CreateSuccessResponse(client)
                    : Response<Client>.CreateWarningResponse("Client not found.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving the client for TenantId: {@tenantId}, DocumentId: {@documentId}", tenantId, documentId);
                return Response<Client>.CreateErrorResponse("An error occurred while retrieving the client.");
            }
        }

        public async Task<Response<bool>> IsClientWhitelistedAsync(string tenantId, string clientId)
        {
            try
            {
                var isWhitelisted = await _clientRepository.IsClientWhitelistedAsync(tenantId, clientId);
                return Response<bool>.CreateSuccessResponse(isWhitelisted);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while checking if client is whitelisted for TenantId: {@tenantId}, ClientId: {@clientId}", tenantId, clientId);
                return Response<bool>.CreateErrorResponse("An error occurred while checking if client is whitelisted.");
            }
        }
    }
}
