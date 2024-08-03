using System;
using Document.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Document.Repository
{
	public class AppDbContext
	{
        private readonly IMongoDatabase _database;

        public AppDbContext(IOptions<MongoSettings> settings, IMongoClient mongoClient)
        {
            _database = mongoClient.GetDatabase(settings.Value.DatabaseName);
        }

        public IMongoCollection<FinancialDocument> FinancialDocuments =>
            _database.GetCollection<FinancialDocument>("FinancialDocuments");

        public IMongoCollection<Client> Clients =>
            _database.GetCollection<Client>("Clients");

        public IMongoCollection<Company> Companies =>
            _database.GetCollection<Company>("Companies");
        public IMongoCollection<Tenant> Tenants =>
            _database.GetCollection<Tenant>("Tenants");

    }
}

