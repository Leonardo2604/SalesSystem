using System.Collections.Generic;
using System.Linq;
using SalesSystem.Models;
using SalesSystem.Data;

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
    }
}
