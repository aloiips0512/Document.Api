using System;
using Document.Models.DTO;

namespace Document.Models
{
	public class DocumentResponse
	{
        public string Data { get; set; }
        public CompanyInfoDto Company { get; set; }
    }
}

