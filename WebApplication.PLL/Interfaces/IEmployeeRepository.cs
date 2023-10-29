using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplicaiton.DAL.Models;

namespace WebApplication.PLL.Interfaces
{
    public interface IEmployeeRepository : IGenericRepository<Employee>
    {
        public IQueryable<Employee> GetEmployeeByAddress(string address);

        public IQueryable<Employee> GetEmployeeByName(string name);
    }
}
