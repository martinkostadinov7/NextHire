using Microsoft.EntityFrameworkCore;
using Data.Abstraction;
using Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly AppDbContext _context;

        public CompanyRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Company> GetByIdAsync(int id)
        {
            return await _context.Companies.FindAsync(id);
        }

        public async Task<List<Company>> GetAllAsync()
        {
            return await _context.Companies.ToListAsync();
        }

        public async Task<Company> AddAsync(Company Company)
        {
            _context.Companies.Add(Company);
            await _context.SaveChangesAsync();
            return Company;
        }

        public async Task<Company> UpdateAsync(Company Company)
        {
            _context.Companies.Update(Company);
            await _context.SaveChangesAsync();
            return Company;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var Company = await _context.Companies.FindAsync(id);

            if (Company == null)
                return false;

            _context.Companies.Remove(Company);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
