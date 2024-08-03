using System;
using Document.Models;
using Document.Repository;
using Document.Services.Interfaces;

namespace Document.Services
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _context;

        public ProductService(AppDbContext context)
        {
            _context = context;
        }

        public Task<Response<bool>> IsProductSupported(int productId)
        {
            throw new NotImplementedException();
        }
    }
}

