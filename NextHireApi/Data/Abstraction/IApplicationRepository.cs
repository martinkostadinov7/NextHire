using Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Abstraction
{
    public interface IApplicationRepository
    {
        Task<Application> GetByIdAsync(int id);
        Task<List<Application>> GetAllAsync();
        Task<Application> AddAsync(Application Application);
        Task<Application> UpdateAsync(Application Application);
        Task<bool> DeleteAsync(int id);
    }
}
