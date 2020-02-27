using System.Collections.Generic;
using System.Linq;
using SalesSystem.Models;
using SalesSystem.Data;

namespace SalesSystem.Services
{
    public class DepartmentService
    {
        private readonly SalesSystemContext _context;

        public DepartmentService(SalesSystemContext context)
        {
            _context = context;
        }

        public List<Department> FindAll()
        {
            return _context.Department.OrderBy(department => department.Name).ToList();
        }
    }
}
