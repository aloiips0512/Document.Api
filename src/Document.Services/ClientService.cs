using System;
using AutoMapper;
using Document.Models;
using Document.Models.DTO;
using Document.Repository;
using Document.Services.Interfaces;
using MongoDB.Driver;

namespace Document.Services
{
    public class ClientService : IClientService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public ClientService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public Task<Response<ClientInfoDto>> GetClientInfoAsync(Guid tenantId, Guid documentId)
        {
            throw new NotImplementedException();
        }

        public Task<Response<CompanyInfoDto>> GetCompanyInfoAsync(string clientVAT)
        {
            throw new NotImplementedException();
        }

        public Task<Response<bool>> IsClientWhitelistedAsync(Guid tenantId, Guid clientId)
        {
            throw new NotImplementedException();
        }
    }
}

