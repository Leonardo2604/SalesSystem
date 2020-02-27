using SalesSystem.Data;
using SalesSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SalesSystem.Services
{
    public class SalesRecordService
    {
        private readonly SalesSystemContext _context;

        public SalesRecordService(SalesSystemContext context)
        {
            _context = context;
        }

        public async Task<List<SalesRecord>> FindByDateAsync(DateTime? minDate, DateTime? maxDate)
        {
            var result = from obj in _context.SalesRecord select obj;
            if (minDate.HasValue)
            {
                result = result.Where(obj => obj.Date >= minDate.Value);
            }

            if (maxDate.HasValue)
            {
                result = result.Where(obj => obj.Date <= maxDate);
            }

            return await result
                .Include(obj => obj.Seller)
                .Include(obj => obj.Seller.Department)
                .OrderByDescending(obj => obj.Date)
                .ToListAsync();
        }

        public async Task<List<IGrouping<Department, SalesRecord>>> FindByDateGroupingAsync(DateTime? minDate, DateTime? maxDate)
        {
            var result = from obj in _context.SalesRecord select obj;
            if (minDate.HasValue)
            {
                result = result.Where(obj => obj.Date >= minDate.Value);
            }

            if (maxDate.HasValue)
            {
                result = result.Where(obj => obj.Date <= maxDate);
            }

            return await result
                .Include(obj => obj.Seller)
                .Include(obj => obj.Seller.Department)
                .OrderByDescending(obj => obj.Date)
                .GroupBy(obj => obj.Seller.Department)
                .ToListAsync();
        }
    }
}
