using System;
using Document.Models;
using Document.Repository;
using Document.Services.Interfaces;

namespace Document.Services
{
    public class ProductService : IProductService
    {
        private readonly List<string> _supportedProducts = new List<string>
        {
            "ProductA",
            "ProductB"
        };
        public Response<bool> IsProductSupported(string productId)
        {
            bool isSupported = _supportedProducts.Contains(productId);
            return isSupported
                ? Response<bool>.CreateSuccessResponse(true)
                : Response<bool>.CreateErrorResponse("Product code is not supported.");
        }
    }
}

