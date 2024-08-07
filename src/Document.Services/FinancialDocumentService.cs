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
using System.Reflection;
using MongoDB.Bson;

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
                    return Response<string>.CreateWarningResponse("Document not found.");
                }

                var product = await _productRepository.GetProductByCodeAsync(productCode);
                if (product == null)
                {
                    return Response<string>.CreateWarningResponse("Product not found.");
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

                foreach (var property in typeof(FinancialData).GetProperties())
                {
                    var propertyName = property.Name.ToLower();
                    var fieldType = productConfig[$"Fields:{propertyName}"] ?? "mask";

                    switch (fieldType.ToLower())
                    {
                        case "hash":
                            if (property.PropertyType == typeof(string))
                            {
                                string originalValue = (string)property.GetValue(document.FinancialData);
                                string hashedValue = Hash(originalValue);
                                property.SetValue(anonymizedDocument, hashedValue);
                            }
                            else
                            {
                                // Log or handle the type mismatch for non-string properties
                                _logger.LogWarning("Property '{PropertyName}' on document type '{DocumentType}' is not of type 'string' for hashing", property.Name, anonymizedDocument.GetType().Name);
                            }
                            break;

                        case "unchanged":
                            property.SetValue(anonymizedDocument, property.GetValue(document.FinancialData));
                            break;

                        case "mask":
                        default:
                            switch (Type.GetTypeCode(property.PropertyType))
                            {
                                case TypeCode.String:
                                    property.SetValue(anonymizedDocument, "#####");
                                    break;
                                case TypeCode.Object when property.PropertyType == typeof(ObjectId):
                                    property.SetValue(anonymizedDocument, ObjectId.GenerateNewId().ToString());
                                    break;
                                case TypeCode.Object when property.PropertyType == typeof(Guid):
                                    property.SetValue(anonymizedDocument, Guid.NewGuid().ToString());
                                    break;
                                default:
                                    property.SetValue(anonymizedDocument, property.GetValue(document.FinancialData));
                                    break;
                            }
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
            byte[] bytes = SHA256.HashData(Encoding.UTF8.GetBytes(input));
            var builder = new StringBuilder();
            foreach (var b in bytes)
            {
                builder.Append(b.ToString("x2"));
            }
            return builder.ToString();
        }
    }
}

