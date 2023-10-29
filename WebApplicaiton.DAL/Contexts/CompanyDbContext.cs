using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplicaiton.DAL.Models;

namespace WebApplicaiton.DAL.Contexts
{
    public class CompanyDbContext:IdentityDbContext<ApplicationUser>
    {
        public CompanyDbContext(DbContextOptions<CompanyDbContext> options):base(options)
        {
        }

        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }

        //public DbSet<IdentityUser> Users { get; set; }
        //public DbSet<IdentityRole> Roles { get; set; }
    }
}
