using System;
using Document.Models;

namespace Document.Services.Interfaces
{
	public interface ICompanyService
	{
        Task<Response<Company>> GetCompanyByRegistrationNumberAsync(string registrationNumber);
    }
}

