using System;
using Document.Models;
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

        public DocumentController(
            IClientService clientService,
            IFinancialDocumentService financialDocumentService,
            IProductService productService,
            ITenantService tenantService)
        {
            _clientService = clientService;
            _financialDocumentService = financialDocumentService;
            _productService = productService;
            _tenantService = tenantService;
        }

        [HttpPost("anonymize")]
        public async Task<IActionResult> AnonymizeDocument(DocumentRequest request)
        {
            if (!_productService.IsProductSupported(request.ProductCode))
            {
                return BadRequest(Response<string>.CreateErrorResponse("Product is not supported"));
            }

            var tenantWhitelisted = await _tenantService.IsTenantWhitelistedAsync(request.TenantId);
            if (!tenantWhitelisted.Data)
            {
                return BadRequest(Response<string>.CreateErrorResponse("Tenant is not whitelisted"));
            }

            var documentResponse = await _financialDocumentService.GetDocumentAsync(request.TenantId, request.DocumentId);
            if (!documentResponse.Success)
            {
                return BadRequest(documentResponse);
            }

            var clientWhitelisted = await _clientService.IsClientWhitelistedAsync(request.TenantId, documentResponse.Data.ClientId);
            if (!clientWhitelisted.Data)
            {
                return BadRequest(Response<string>.CreateErrorResponse("Client is not whitelisted"));
            }

            var companyInfo = await _clientService.GetCompanyInfoAsync(documentResponse.Data.ClientVAT);
            if (!companyInfo.Success)
            {
                return BadRequest(companyInfo);
            }

            var anonymizedData = await _financialDocumentService.AnonymizeDocumentAsync(documentResponse.Data);
            if (!anonymizedData.Success)
            {
                return BadRequest(anonymizedData);
            }

            var response = new DocumentResponse
            {
                // Document= documentResponse.Data,
                Company = companyInfo.Data,
                Data = anonymizedData.Data
            };

            return Ok(Response<DocumentResponse>.CreateSuccessResponse(response));
        }
    }
}

