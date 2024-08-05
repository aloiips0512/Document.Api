using System;
using Document.Models;
using Document.Repository;
using Document.Repository.Interfaces;
using Document.Services.Interfaces;
using MongoDB.Driver;

namespace Document.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
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
                return Response<bool>.CreateErrorResponse($"An error occurred: {ex.Message}");
            }
        }
    }
}

