using Document.Models;

namespace Document.Services.Interfaces;

public interface IProductService
{
    Response<bool> IsProductSupported(string productId);
}

