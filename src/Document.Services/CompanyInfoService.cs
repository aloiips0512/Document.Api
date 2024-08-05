using System;
using Document.Models;
using Document.Services.Interfaces;

namespace Document.Services
{
    public class CompanyInfoService : ICompanyInfoService
    {
        // Simulated company information
        private readonly Dictionary<string, (string registrationNumber, string companyType)> _companyData = new()
    {
        { "VAT123", ("REG123", "large") }, // Example VATs
        { "VAT456", ("REG456", "small") }
    };

        public Response<(string registrationNumber, string companyType)> GetCompanyInfo(string clientVAT)
        {
            if (_companyData.TryGetValue(clientVAT, out var companyInfo))
            {
                return Response<(string registrationNumber, string companyType)>.CreateSuccessResponse(companyInfo);
            }
            return Response<(string registrationNumber, string companyType)>.CreateErrorResponse("Company information not found.");
        }
    }
}

