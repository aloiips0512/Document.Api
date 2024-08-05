using System;
using Document.Models;

namespace Document.Repository.Interfaces
{
	public interface IProductRepository
	{
        Task<bool> IsProductSupportedAsync(string productCode);
        Task CreateProductAsync(Product product);
    }
}

