using Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Abstraction
{
    public interface ICvRepository
    {
        Task<Cv> GetByIdAsync(int id);
        Task<List<Cv>> GetAllAsync();
        Task<Cv> AddAsync(Cv Cv);
        Task<Cv> UpdateAsync(Cv Cv);
        Task<bool> DeleteAsync(int id);
    }
}
