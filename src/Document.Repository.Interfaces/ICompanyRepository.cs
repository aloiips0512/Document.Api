using System;
using Document.Models;

namespace Document.Repository.Interfaces
{
	public interface ICompanyRepository
	{
        Task<Company> GetCompanyByRegistrationNumberAsync(string registrationNumber);
        Task CreateCompanyAsync(Company company);
    }
}

