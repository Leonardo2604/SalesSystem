using System.Collections.Generic;
using System.Linq;
using SalesSystem.Models;
using SalesSystem.Data;
using Microsoft.EntityFrameworkCore;
using SalesSystem.Services.Exceptions;
using System.Threading.Tasks;

namespace SalesSystem.Services
{
    public class SellerService
    {
        private readonly SalesSystemContext _context;

        public SellerService(SalesSystemContext context)
        {
            _context = context;
        }

        public async Task<List<Seller>> FindAllAsync()
        {
            return await _context.Seller.ToListAsync();
        }

        public async Task InsertAsync(Seller seller)
        {
            _context.Add(seller);
            await _context.SaveChangesAsync();
        }

        public async Task<Seller> FindByIdAsync(int sellerId)
        {
            return await _context.Seller.Include(seller => seller.Department).FirstOrDefaultAsync(seller => seller.Id == sellerId);
        }

        public async Task RemoveAsync(Seller seller)
        {
            try 
            {
                _context.Seller.Remove(seller);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                throw new IntegrityException(e.Message);
            }  
        }

        public async Task RemoveAsync(int sellerId)
        {
            Seller seller = await _context.Seller.FindAsync(sellerId);
            await RemoveAsync(seller);
        }

        public async void UpdateAsync(Seller seller)
        {
            bool hasAny = await _context.Seller.AnyAsync(item => item.Id == seller.Id);
            if (!hasAny)
            {
                throw new NotFoundException("Id not found");
            }

            try
            {
                _context.Update(seller);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }
            
        }
    }
}
