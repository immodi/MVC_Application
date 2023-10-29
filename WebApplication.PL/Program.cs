using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplicaiton.DAL.Contexts;
using WebApplication.PLL.Interfaces;
using WebApplication.PLL.Repositories;
using WebApplication.PL.MapperProfiles;
using WebApplicaiton.DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace WebApplication.PL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = Microsoft.AspNetCore.Builder.WebApplication.CreateBuilder(args);

            #region Configure Services and Allow Depenedacy Injection
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<CompanyDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            //services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            //services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddAutoMapper(m => m.AddProfile(new EmployeeProfile()));
            builder.Services.AddAutoMapper(m => m.AddProfile(new UserProfile()));
            builder.Services.AddAutoMapper(m => m.AddProfile(new RoleProfile()));


            //services.AddScoped<UserManager<ApplicationUser>>();
            //services.AddScoped<SignInManager<ApplicationUser>>();
            //services.AddScoped<RoleManager<IdentityRole>>();

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireDigit = true;
                options.Password.RequireUppercase = true;
            }).AddEntityFrameworkStores<CompanyDbContext>()
              .AddDefaultTokenProviders(); //Default Tokens

            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                    .AddCookie(options =>
                    {
                        options.LoginPath = "Account/Login";
                        options.AccessDeniedPath = "Home/Error";
                    });
            #endregion

            var app = builder.Build();
            var env = builder.Environment;

            #region Http Requests Piplines

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}"
                    );
            });
            #endregion

            app.Run();
        }
    }
}
