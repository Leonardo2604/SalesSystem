using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    }
}
