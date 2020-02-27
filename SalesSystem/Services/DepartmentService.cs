using System.Collections.Generic;
using System.Linq;
using SalesSystem.Models;
using SalesSystem.Data;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SalesSystem.Services
{
    public class DepartmentService
    {
        private readonly SalesSystemContext _context;

        public DepartmentService(SalesSystemContext context)
        {
            _context = context;
        }

        public async Task<List<Department>> FindAllAsync()
        {
            return await _context.Department.OrderBy(department => department.Name).ToListAsync();
        }
    }
}
