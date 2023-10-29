using Microsoft.EntityFrameworkCore;
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
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {

        private readonly CompanyDbContext context;

        public GenericRepository(CompanyDbContext context)
        {
            this.context = context;
        }

        public async Task Add(T item)
        {
            await context.Set<T>().AddAsync(item);
        }

        public void Delete(T item)
        {
            context.Set<T>().Remove(item);
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            if (typeof(T) == typeof(Employee))
            {
                return (IEnumerable<T>) await context.Employees.Include(e => e.Department).ToListAsync();
            }
            else
            {
                return await context.Set<T>().ToListAsync();
            }
        }

        public async Task<T> GetById(int id)
        =>  await context.Set<T>().FindAsync(id);
        
        public void Update(T item)
        {
            context.Set<T>().Update(item);
        }
    }
}
