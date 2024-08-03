using System;
using AutoMapper;
using Document.Models;
using Document.Models.DTO;
using MongoDB.Driver.Linq;
using Document.Repository;
using Document.Services.Interfaces;
using MongoDB.Bson.IO;
using MongoDB.Driver;
using Newtonsoft.Json;
using MongoDB.Driver.Linq;

namespace Document.Services
{
    public class FinancialDocumentService : IFinancialDocumentService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public FinancialDocumentService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public Task<string> AnonymizeDocument(string documentJson, string productCode)
        {
            throw new NotImplementedException();
        }

        public Task<Response<string>> GetDocumentAsync(Guid tenantId, Guid documentId)
        {
            throw new NotImplementedException();
        }
    }
}

