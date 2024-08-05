using System;
using System.Linq;
using AutoMapper;
using Document.Models;
using Document.Repository;
using Document.Repository.Interfaces;
using Document.Services.Interfaces;
using MongoDB.Driver;

namespace Document.Services
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _clientRepository;

        public ClientService(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public async Task<Response<Client>> GetClientByTenantAndDocumentIdAsync(string tenantId, string documentId)
        {
            var client = await _clientRepository.GetClientByTenantAndDocumentIdAsync(tenantId, documentId);
            return client != null
                ? Response<Client>.CreateSuccessResponse(client)
                : Response<Client>.CreateErrorResponse("Client not found.");
        }

        public async Task<Response<bool>> IsClientWhitelistedAsync(string tenantId, string clientId)
        {
            var isWhitelisted = await _clientRepository.IsClientWhitelistedAsync(tenantId, clientId);
            return isWhitelisted
                ? Response<bool>.CreateSuccessResponse(true)
                : Response<bool>.CreateErrorResponse("Client not whitelisted.");
        }
    }
}
