using Microsoft.AspNetCore.Mvc;
using Service.Abstraction;
using Shared.DTOs.Companies;

namespace API.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class CompaniesController(ICompanyService companyService) : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<CompanyReadDto>> CreateCompany(CompanyCreateDto request)
        {
            CompanyReadDto result = await companyService.CreateCompanyAsync(request);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CompanyReadDto>> GetCompanyById(int id)
        {
            CompanyReadDto result = await companyService.GetCompanyById(id);
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<List<CompanyReadDto>>> GetAll()
        {
            List<CompanyReadDto> result = await companyService.GetAllCompanys();
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<CompanyReadDto>> DeleteCompany(int id)
        {
            await companyService.DeleteCompanyAsync(id);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<CompanyReadDto>> UpdateCompany(int id, [FromBody] CompanyUpdateDto request)
        {
            CompanyReadDto result = await companyService.UpdateCompanyAsync(id, request);
            return Ok(result);
        }
    }
}
