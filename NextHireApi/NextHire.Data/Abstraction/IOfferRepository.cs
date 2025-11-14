using NextHire.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextHire.Data.Abstraction
{
    public interface IOfferRepository
    {
        Task<Offer> GetByIdAsync(int id);
        Task<IEnumerable<Offer>> GetAllAsync();
        Task<Offer> AddAsync(Offer Offer);
        Task<Offer> UpdateAsync(Offer Offer);
        Task<bool> DeleteAsync(int id);
    }
}
