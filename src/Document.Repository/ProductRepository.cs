using System;
using Document.Models;
using Document.Models.Settings;
using Document.Repository.Interfaces;
using MongoDB.Driver;

namespace Document.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly IMongoCollection<Product> _products;

        public ProductRepository(IMongoDatabase database)
        {
            _products = database.GetCollection<Product>("Products");
        }
        public async Task<Product> GetProductByCodeAsync(string productCode)
        {
            return await _products.Find(product => product.ProductCode == productCode).FirstOrDefaultAsync();
        }

        public async Task<bool> IsProductSupportedAsync(string productCode)
        {
            var result = await _products.Find(product => product.ProductCode == productCode).FirstOrDefaultAsync();
            return result != null;
        }
    }
}

