using NextHire.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextHire.Data.Abstraction
{
    public interface IApplicationRepository
    {
        Task<Application> GetByIdAsync(int id);
        Task<IEnumerable<Application>> GetAllAsync();
        Task<Application> AddAsync(Application Application);
        Task<Application> UpdateAsync(Application Application);
        Task<bool> DeleteAsync(int id);
    }
}
