using System;
using AutoMapper;
using Document.Models;
using MongoDB.Driver.Linq;
using Document.Repository;
using Document.Services.Interfaces;
using MongoDB.Bson.IO;
using MongoDB.Driver;
using Newtonsoft.Json;
using System.Text;
using System.Text.Json;
using ThirdParty.Json.LitJson;

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

        public Task<Response<string>> AnonymizeDocument(string documentJson, string productCode)
        {
            throw new NotImplementedException();
        }

        public Task<Response<string>> GetDocumentAsync(Guid tenantId, Guid documentId, string productCode)
        {
            throw new NotImplementedException();
        }
    }
}

