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
    public class CvRepository : ICvRepository
    {
        private readonly AppDbContext _context;

        public CvRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Cv> GetByIdAsync(int id)
        {
            return await _context.Cvs.FindAsync(id);
        }

        public async Task<List<Cv>> GetAllAsync()
        {
            return await _context.Cvs.ToListAsync();
        }

        public async Task<Cv> AddAsync(Cv cv)
        {
            _context.Cvs.Add(cv);
            await _context.SaveChangesAsync();
            return cv;
        }

        public async Task<Cv> UpdateAsync(Cv cv)
        {
            _context.Cvs.Update(cv);
            await _context.SaveChangesAsync();
            return cv;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var cv = await _context.Cvs.FindAsync(id);

            if (cv == null)
                return false;

            _context.Cvs.Remove(cv);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
