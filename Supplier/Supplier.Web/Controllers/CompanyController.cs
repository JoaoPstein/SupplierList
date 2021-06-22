using Microsoft.AspNetCore.Mvc;
using Supplier.Application.DTOs.Companys;
using Supplier.Application.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Supplier.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : Controller 
    {
        private readonly ICompanyService _companyService;

        public CompanyController(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        [HttpPost]
        public IActionResult CreateCompany([FromBody] CompanyRequestDto requestDto)
        {
            _companyService.Create(requestDto);
            return Ok();
        }

        [HttpDelete]
        [Route("/{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            await _companyService.Delete(id);
            return Ok();
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<CompanyResponseDto> GetById([FromRoute] Guid id)
        {
            return await _companyService.GetById(id);
        }

        [HttpGet]
        public Task<IList<CompanyResponseDto>> GetAll()
        {
            return _companyService.GetAll();
        }

        [HttpPut]
        [Route("/{id}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] CompanyRequestDto requestDto)
        {
            await _companyService.Update(id, requestDto);
            return Ok();
        }
    }
}
