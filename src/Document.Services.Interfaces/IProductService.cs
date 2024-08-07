using Document.Models;

namespace Document.Services.Interfaces;

public interface IProductService
{
    Task<Response<bool>> IsProductSupported(string productId);
}

