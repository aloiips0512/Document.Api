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
using Document.Repository.Interfaces;
using System.Security.Cryptography;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;

namespace Document.Services
{
    public class FinancialDocumentService : IFinancialDocumentService
    {
        private readonly IDocumentRepository _documentRepository;
        private readonly IProductRepository _productRepository;
        private readonly ILogger<FinancialDocumentService> _logger;
        private readonly IConfiguration _configuration;

        public FinancialDocumentService(IDocumentRepository documentRepository,
                                        IProductRepository productRepository,
                                        ILogger<FinancialDocumentService> logger,
                                        IConfiguration configuration)
        {
            _documentRepository = documentRepository;
            _productRepository = productRepository;
            _logger = logger;
            _configuration = configuration;
        }

        public async Task<Response<string>> GetFinancialDocumentAsync(string tenantId, string documentId, string productCode)
        {
            try
            {
                var document = await _documentRepository.GetDocumentByTenantAndDocumentIdAsync(tenantId, documentId);
                if (document == null)
                {
                    return Response<string>.CreateErrorResponse("Document not found.");
                }

                var product = await _productRepository.GetProductByCodeAsync(productCode);
                if (product == null)
                {
                    return Response<string>.CreateErrorResponse("Product not found.");
                }

                var anonymizedDocument = AnonymizeFinancialData(document, product);
                return Response<string>.CreateSuccessResponse(anonymizedDocument);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving the financial document for TenantId: {TenantId}, DocumentId: {DocumentId}, ProductCode: {ProductCode}", tenantId, documentId, productCode);
                return Response<string>.CreateErrorResponse("An error occurred while retrieving the financial document.");
            }
        }

        private string AnonymizeFinancialData(FinancialDocument document, Product product)
        {
            try
            {
                var anonymizedDocument = new FinancialData
                {
                    AccountNumber = document.FinancialData.AccountNumber, 
                    Balance = document.FinancialData.Balance,
                    Currency = document.FinancialData.Currency,
                    Transactions = document.FinancialData.Transactions.Select(t => new Transaction
                    {
                        TransactionId = "#####",
                        Amount = t.Amount,
                        Date = t.Date,
                        Description = "#####",
                        Category = t.Category
                    }).ToList()
                };

                var productConfig = _configuration.GetSection($"Products:{product.ProductCode}");

                foreach (var property in typeof(FinancialDocument).GetProperties())
                {
                    var propertyName = property.Name.ToLower();
                    var fieldType = productConfig[$"Fields:{propertyName}"] ?? "mask";

                    switch (fieldType)
                    {
                        case "hash":
                            anonymizedDocument.GetType().GetProperty(property.Name).SetValue(anonymizedDocument, Hash(document.GetType().GetProperty(property.Name).GetValue(document).ToString()));
                            break;
                        case "unchanged":
                            anonymizedDocument.GetType().GetProperty(property.Name).SetValue(anonymizedDocument, document.GetType().GetProperty(property.Name).GetValue(document));
                            break;
                        default:
                            anonymizedDocument.GetType().GetProperty(property.Name).SetValue(anonymizedDocument, "#####");
                            break;
                    }
                }

                return Newtonsoft.Json.JsonConvert.SerializeObject(anonymizedDocument);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while anonymizing the financial data for ProductCode: {ProductCode}", product.ProductCode);
                throw;
            }
        }

        private string Hash(string input)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
                var builder = new StringBuilder();
                foreach (var b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }

    }
}

