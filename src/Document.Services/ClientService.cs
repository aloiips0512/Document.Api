using System;
using System.Linq;
using AutoMapper;
using Document.Models;
using Document.Repository;
using Document.Services.Interfaces;
using MongoDB.Driver;

namespace Document.Services
{
    public class ClientService : IClientService
    {

        public ClientService()
        {

        }

        public Task<Response<Client>> GetClientInfoAsync(Guid tenantId, Guid documentId)
        {
            throw new NotImplementedException();
        }

        public Task<Response<Company>> GetCompanyInfoAsync(string clientVAT)
        {
            throw new NotImplementedException();
        }

        public Task<Response<bool>> IsClientWhitelistedAsync(Guid tenantId, Guid clientId)
        {
            throw new NotImplementedException();
        }
    }
}
