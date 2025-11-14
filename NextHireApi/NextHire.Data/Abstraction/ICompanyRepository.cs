using NextHire.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextHire.Data.Abstraction
{
    public interface ICompanyRepository
    {
        Task<Company> GetByIdAsync(int id);
        Task<IEnumerable<Company>> GetAllAsync();
        Task<Company> AddAsync(Company Company);
        Task<Company> UpdateAsync(Company Company);
        Task<bool> DeleteAsync(int id);
    }
}
