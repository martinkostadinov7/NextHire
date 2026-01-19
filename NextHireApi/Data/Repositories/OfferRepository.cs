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
    public class OfferRepository : IOfferRepository
    {
        private readonly AppDbContext _context;

        public OfferRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Offer> GetByIdAsync(int id)
        {
            return await _context.Offers.FindAsync(id);
        }

        public async Task<List<Offer>> GetAllAsync()
        {
            return await _context.Offers.ToListAsync();
        }

        public async Task<Offer> AddAsync(Offer offer)
        {
            _context.Offers.Add(offer);
            await _context.SaveChangesAsync();
            return offer;
        }

        public async Task<Offer> UpdateAsync(Offer offer)
        {
            _context.Offers.Update(offer);
            await _context.SaveChangesAsync();
            return offer;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var offer = await _context.Offers.FindAsync(id);

            if (offer == null)
                return false;

            _context.Offers.Remove(offer);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
