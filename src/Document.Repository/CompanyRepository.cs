using System;
using Document.Models;
using Document.Repository.Interfaces;
using MongoDB.Driver;

namespace Document.Repository
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly IMongoCollection<Company> _companies;
        private readonly IMongoCollection<Client> _clients;

        public CompanyRepository(IMongoDatabase database)
        {
            _companies = database.GetCollection<Company>("Companies");
            _clients = database.GetCollection<Client>("Clients");
        }

        public async Task<Company> GetCompanyByClientVATAsync(string clientVAT)
        {
            // find client by ClientVAT
            var client = await _clients.Find(c => c.ClientVAT == clientVAT).FirstOrDefaultAsync();
            if (client == null)
            {
                return null;
            }

            // find the company by client's id
            var company = await _companies.Find(c => c.ClientId == client.ClientId).FirstOrDefaultAsync();
            if (company == null)
            {
                return null;
            }

            return company;
        }

    }
}

