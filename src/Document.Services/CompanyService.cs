using System;
using Document.Models;
using Document.Repository.Interfaces;
using Document.Services.Interfaces;

namespace Document.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;

        public CompanyService(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        public async Task<Response<Company>> GetCompanyByRegistrationNumberAsync(string registrationNumber)
        {
            var company = await _companyRepository.GetCompanyByRegistrationNumberAsync(registrationNumber);
            return company != null
                ? Response<Company>.CreateSuccessResponse(company)
                : Response<Company>.CreateErrorResponse("Company not found.");
        }
    }
}

