using System;
using Document.Models;
using Document.Services;
using Document.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Document.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DocumentController : ControllerBase
    {
        private readonly IClientService _clientService;
        private readonly IFinancialDocumentService _financialDocumentService;
        private readonly IProductService _productService;
        private readonly ITenantService _tenantService;
        private readonly ICompanyService _companyService;

        public DocumentController(
            IClientService clientService,
            IFinancialDocumentService financialDocumentService,
            IProductService productService,
            ITenantService tenantService,
            ICompanyService companyService)
        {
            _clientService = clientService;
            _financialDocumentService = financialDocumentService;
            _productService = productService;
            _tenantService = tenantService;
            _companyService = companyService;
        }

        [HttpPost]
        public async Task<IActionResult> GetFinancialDocument([FromBody] DocumentRequest request)
        {
            // Step 1: Validate Product Code
            var productResponse = await _productService.IsProductSupported(request.ProductCode);
            if (!productResponse.Success) return StatusCode(500, productResponse.ErrorMessage);
            if (productResponse.Success && !productResponse.Data) return StatusCode(403, productResponse.WarningMessage);

            // Step 2: Tenant ID Whitelisting
            var tenantResponse = await _tenantService.IsTenantWhitelistedAsync(request.TenantId);
            if (!tenantResponse.Success) return StatusCode(500, tenantResponse.ErrorMessage);
            if (tenantResponse.Success && !tenantResponse.Data) return StatusCode(403, tenantResponse.WarningMessage);

            // Step 3: Client ID Whitelisting
            var clientResponse = await _clientService.GetClientByTenantAndDocumentIdAsync(request.TenantId, request.DocumentId);
            //if (clientResponse.Success && clientResponse.Data != null) return StatusCode(403, clientResponse.ErrorMessage);
            if (!clientResponse.Success) return StatusCode(500, clientResponse.ErrorMessage);
            var client = clientResponse.Data;

            var clientWhitelistResponse = await _clientService.IsClientWhitelistedAsync(request.TenantId, client.ClientId);
            if (!clientWhitelistResponse.Success) return StatusCode(500, clientWhitelistResponse.ErrorMessage);
            if (clientWhitelistResponse.Success && !clientWhitelistResponse.Data) return StatusCode(403, clientWhitelistResponse.WarningMessage);

            // Step 4: Fetch Additional Client Information
            var companyResponse = await _companyService.GetCompanyByClientVATAsync(client.ClientVAT);
            if (!companyResponse.Success) return StatusCode(500, companyResponse.ErrorMessage);

            var company = companyResponse.Data;

            // Step 5: Company Type Check
            if (company.CompanyType == "small") return StatusCode(403, "Company type is small.");

            // Step 6: Retrieve Financial Document for Client
            var documentResponse = await _financialDocumentService.GetFinancialDocumentAsync(request.TenantId, request.DocumentId, request.ProductCode);
            if (!documentResponse.Success) return StatusCode(500, documentResponse.ErrorMessage);
            if (documentResponse.Success && documentResponse.Data == null) return StatusCode(403, documentResponse.WarningMessage);

            var document = documentResponse.Data;

            // Step 7: Enrich Response Model
            var responseModel = new DocumentResponse
            {
                Company = company,
                Data = document
            };

            return Ok(Response<DocumentResponse>.CreateSuccessResponse(responseModel));
        }

    }
}

