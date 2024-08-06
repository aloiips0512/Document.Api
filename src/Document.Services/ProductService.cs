using System;
using Document.Models;
using Document.Repository;
using Document.Repository.Interfaces;
using Document.Services.Interfaces;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;

namespace Document.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly ILogger<ProductService> _logger;

        public ProductService(IProductRepository productRepository, ILogger<ProductService> logger)
        {
            _productRepository = productRepository;
            _logger = logger;
        }

        public async Task<Response<bool>> IsProductSupported(string productCode)
        {
            try
            {
                var isSupported = await _productRepository.IsProductSupportedAsync(productCode);
                return Response<bool>.CreateSuccessResponse(isSupported);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while checking if product is supported for ProductCode: {@productCode}", productCode);
                return Response<bool>.CreateErrorResponse("An error occurred while checking if product is supported.");
            }
        }
    }
}

