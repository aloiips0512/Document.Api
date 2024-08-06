using System;
using Document.Models;
using Document.Repository.Interfaces;
using Document.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace Document.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly ILogger<CompanyService> _logger;

        public CompanyService(ICompanyRepository companyRepository, ILogger<CompanyService> logger)
        {
            _companyRepository = companyRepository;
            _logger = logger;
        }

        public async Task<Response<Company>> GetCompanyByClientVATAsync(string clientVAT)
        {
            try
            {
                var company = await _companyRepository.GetCompanyByClientVATAsync(clientVAT);
                return company != null
                    ? Response<Company>.CreateSuccessResponse(company)
                    : Response<Company>.CreateWarningResponse("Company not found.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving the company for Registration number: {@registrationNumber}", clientVAT);
                return Response<Company>.CreateErrorResponse("An error occurred while retrieving the company.");
            }

        }
    }
}

