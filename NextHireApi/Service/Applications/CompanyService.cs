using Business.Entities;
using Data.Abstraction;
using Service.Abstraction;
using Shared.DTOs.Companies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Applications
{
    public class CompanyService : ICompanyService
    {
        private ICompanyRepository companyRepository;

        public CompanyService(ICompanyRepository companyRepository)
        {
            this.companyRepository = companyRepository;
        }

        public async Task<CompanyReadDto> CreateCompanyAsync(CompanyCreateDto companyDto, int senderId)
        {
            Company company = new Company(
                companyDto.Name, companyDto.Description);

            company = await companyRepository.AddAsync(company);

            CompanyReadDto companyReadDto = new CompanyReadDto(company.Id,
                company.Name, company.Description, company.Offers);

            return companyReadDto;
        }

        public async Task DeleteCompanyAsync(int id)
        {
            await companyRepository.DeleteAsync(id);
        }

        public async Task<List<CompanyReadDto>> GetAllCompanys()
        {
            List<Company> companys = await companyRepository.GetAllAsync();

            List<CompanyReadDto> companyReadDtos = new List<CompanyReadDto>();
            foreach (var company in companys)
            {
                CompanyReadDto companyReadDto = new CompanyReadDto(company.Id,
               company.Name, company.Description, company.Offers);

                companyReadDtos.Add(companyReadDto);
            }

            return companyReadDtos;
        }

        public async Task<CompanyReadDto> GetCompanyById(int id)
        {
            Company company = await companyRepository.GetByIdAsync(id);

            CompanyReadDto companyReadDto = new CompanyReadDto(company.Id,
                company.Name, company.Description, company.Offers);

            return companyReadDto;
        }

        public async Task<CompanyReadDto> UpdateCompanyAsync(int id, CompanyUpdateDto companyDto)
        {
            Company company = await companyRepository.GetByIdAsync(id);

            if (company == null)
                throw new Exception("Company not found");

            company.Name = companyDto.Name;
            company.Description = companyDto.Description;

            company = await companyRepository.UpdateAsync(company);

            return new CompanyReadDto(
                company.Id,company.Name,company.Description,company.Offers
            );
        }

    }
}
