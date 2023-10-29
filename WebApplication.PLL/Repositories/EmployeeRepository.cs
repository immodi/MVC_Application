using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplicaiton.DAL.Contexts;
using WebApplicaiton.DAL.Models;
using WebApplication.PLL.Interfaces;

namespace WebApplication.PLL.Repositories
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        private readonly CompanyDbContext context;

        public EmployeeRepository(CompanyDbContext context):base(context)
        {
            this.context = context;
        }

        public IQueryable<Employee> GetEmployeeByName(string name)
        {
            return context.Employees.Where(e => e.Name.ToLower() == name.ToLower());
        }

        IQueryable<Employee> IEmployeeRepository.GetEmployeeByAddress(string address)
        {
            return context.Employees.Where(e => e.Address == address);
        }
    }
}
