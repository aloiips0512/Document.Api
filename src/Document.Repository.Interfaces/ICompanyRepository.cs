using System;
using Document.Models;

namespace Document.Repository.Interfaces
{
	public interface ICompanyRepository
	{
        Task<Company> GetCompanyByClientVATAsync(string registrationNumber);
    }
}

