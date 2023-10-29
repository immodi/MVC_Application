using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplicaiton.DAL.Contexts;
using WebApplication.PLL.Interfaces;

namespace WebApplication.PLL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CompanyDbContext _context;

        public IEmployeeRepository EmployeeRepository { get ; set ; }
        public IDepartmentRepository DepartmentRepository { get ; set ; }

        public UnitOfWork(CompanyDbContext context)
        {
            EmployeeRepository = new EmployeeRepository(context);
            DepartmentRepository = new DepartmentRepository(context);
            this._context = context;
        }

        public async Task<int> Complete()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
