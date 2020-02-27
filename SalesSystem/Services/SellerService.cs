using System.Collections.Generic;
using System.Linq;
using SalesSystem.Models;
using SalesSystem.Data;
using Microsoft.EntityFrameworkCore;
using SalesSystem.Services.Exceptions;

namespace SalesSystem.Services
{
    public class SellerService
    {
        private readonly SalesSystemContext _context;

        public SellerService(SalesSystemContext context)
        {
            _context = context;
        }

        public List<Seller> FindAll()
        {
            return _context.Seller.ToList();
        }

        public void Insert(Seller seller)
        {
            _context.Add(seller);
            _context.SaveChanges();
        }

        public Seller FindById(int sellerId)
        {
            return _context.Seller.Include(seller => seller.Department).FirstOrDefault(seller => seller.Id == sellerId);
        }

        public void Remove(Seller seller)
        {
            _context.Seller.Remove(seller);
            _context.SaveChanges();
        }

        public void Remove(int sellerId)
        {
            Seller seller = _context.Seller.Find(sellerId);
            Remove(seller);
        }

        public void Update(Seller seller)
        {
            if (!_context.Seller.Any(item => item.Id == seller.Id))
            {
                throw new NotFoundException("Id not found");
            }

            try
            {
                _context.Update(seller);
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }
            
        }
    }
}
