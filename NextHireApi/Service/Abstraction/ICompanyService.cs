using Shared.DTOs.Companies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Abstraction
{
    public interface ICompanyService
    {
        Task<CompanyReadDto> CreateCompanyAsync(CompanyCreateDto companyDto, int senderId);
        Task<CompanyReadDto> UpdateCompanyAsync(int id, CompanyUpdateDto companyDto);
        Task DeleteCompanyAsync(int id);
        Task<List<CompanyReadDto>> GetAllCompanys();
        Task<CompanyReadDto> GetCompanyById(int id);
    }
}
