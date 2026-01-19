using Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Abstraction
{
    public interface IOfferRepository
    {
        Task<Offer> GetByIdAsync(int id);
        Task<List<Offer>> GetAllAsync();
        Task<Offer> AddAsync(Offer Offer);
        Task<Offer> UpdateAsync(Offer Offer);
        Task<bool> DeleteAsync(int id);
    }
}
