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
        { return Ok(); }

    }
}

