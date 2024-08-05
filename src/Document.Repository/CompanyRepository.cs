using System;
using Document.Models;
using Document.Repository.Interfaces;
using MongoDB.Driver;

namespace Document.Repository
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly IMongoCollection<Company> _companies;

        public CompanyRepository(IMongoDatabase database)
        {
            _companies = database.GetCollection<Company>("Companies");
        }

        public async Task<Company> GetCompanyByRegistrationNumberAsync(string registrationNumber)
        {
            return await _companies.Find(company => company.RegistrationNumber == registrationNumber).FirstOrDefaultAsync();
        }

        public async Task CreateCompanyAsync(Company company)
        {
            await _companies.InsertOneAsync(company);
        }
    }
}

