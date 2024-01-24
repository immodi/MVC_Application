using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplicaiton.DAL.Contexts;
using WebApplicaiton.DAL.Models;
using WebApplication.PL.Helpers;
using WebApplication.PL.ViewModels;
using WebApplication.PLL.Interfaces;

namespace WebApplication.PL.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper _mapper;

        public EmployeeController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this._mapper = mapper;
        }

        // GET: Employee
        public async Task<IActionResult> Index(string SearchValue)
        {
            IEnumerable<Employee> employees;

            if (string.IsNullOrEmpty(SearchValue))
                employees = await unitOfWork.EmployeeRepository.GetAll();
            else
                employees = unitOfWork.EmployeeRepository.GetEmployeeByName(SearchValue);

            var mappedEmployee = _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(employees);

            return View(mappedEmployee);
        }

        // GET: Employee/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var employee = await unitOfWork.EmployeeRepository.GetById(id);
            if (employee is null)
            {
                return NotFound();
            }
            var mappedEmployee = _mapper.Map<Employee, EmployeeViewModel>(employee);
            return View(mappedEmployee);
        }

        // GET: Employee/Create
        public IActionResult Create()
        {
            ViewBag.Departments = unitOfWork.DepartmentRepository.GetAll();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EmployeeViewModel employeeViewModel)
        {
            #region Manual Mapping
            //Employee employee = new()
            //{
            //    Name = employeeViewModel.Name,
            //    Age = employeeViewModel.Age,
            //    Salary = employeeViewModel.Salary,
            //    Email = employeeViewModel.Email,
            //    PhoneNumber = employeeViewModel.PhoneNumber,
            //    Address = employeeViewModel.Address,
            //    IsActive = employeeViewModel.IsActive,
            //    HireDate = employeeViewModel.HireDate,
            //    DepartmentId = employeeViewModel.DepartmentId
            //}; 
            #endregion
            var employee = _mapper.Map<EmployeeViewModel, Employee>(employeeViewModel);
            employee.ImageName = DocumentSettings.UploadImage(employeeViewModel.Image, "images");
            ModelState.Remove("ImageName");
            if (employeeViewModel.DepartmentId != null)
            {
                employee.Department = await unitOfWork.DepartmentRepository.GetById((int) employeeViewModel.DepartmentId);
                ModelState.Remove("Department");
            }
            try
            {
                if (ModelState.IsValid)
                {
                    await unitOfWork.EmployeeRepository.Add(employee);
                    int output = await unitOfWork.Complete();
                    TempData["Message"] = "Employee has been created!";
                    return RedirectToAction(nameof(Index));
                }
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                foreach (var error in errors)
                {
                    await Console.Out.WriteLineAsync(error.ErrorMessage);
                }
            }
            catch (Exception)
            {
                return View();
            }
            return View(employeeViewModel);
        }
        public async Task<IActionResult> Edit(int id)
        {
            var employee = await unitOfWork.EmployeeRepository.GetById(id);
            ViewBag.Departments = unitOfWork.DepartmentRepository.GetAll();
            var employeeViewModel = _mapper.Map<Employee, EmployeeViewModel>(employee);

            return View(employeeViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EmployeeViewModel employeeViewModel)
        {

            if (id != employeeViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var employee = _mapper.Map<EmployeeViewModel, Employee>(employeeViewModel);
                    unitOfWork.EmployeeRepository.Update(employee);
                    int output = await unitOfWork.Complete();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(String.Empty, ex.Message);
                }
            }
            return View(employeeViewModel);
        }

        public async Task<IActionResult> Delete(int id)
        {

            var employee = await unitOfWork.EmployeeRepository.GetById(id);

            if (employee == null)
            {
                return NotFound();
            }
            var employeeViewModel = _mapper.Map<Employee, EmployeeViewModel>(employee);
            if (employeeViewModel.ImageName is not null)
            {
                DocumentSettings.DeleteImage(employeeViewModel.ImageName, "images");
            }
            return View(employeeViewModel);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, EmployeeViewModel employeeViewModel)
        {
            if (id != employeeViewModel.Id)
                return BadRequest();
            try
            {
                var employee = _mapper.Map<EmployeeViewModel, Employee>(employeeViewModel);
                unitOfWork.EmployeeRepository.Delete(employee);
                int output = await unitOfWork.Complete();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(String.Empty, ex.Message);
            }
            return View(employeeViewModel);
        }

        private async Task<bool> EmployeeExists(int id)
        {
            var employees = await unitOfWork.EmployeeRepository.GetAll();
            return employees.Any(e => e.Id == id);
        }
    }
}
