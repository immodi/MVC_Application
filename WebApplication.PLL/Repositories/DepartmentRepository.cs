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
    public class DepartmentRepository : GenericRepository<Department>, IDepartmentRepository
    {
        public DepartmentRepository(CompanyDbContext context):base(context)
        {
        }  
    }
}
