using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.PL.ViewModels;

namespace WebApplication.PL.Controllers
{
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;

        public RoleController(RoleManager<IdentityRole> roleManager, IMapper mapper)
        {
            _roleManager = roleManager;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                var users = await _roleManager.Roles.Select(r => new RoleViewModel
                {
                    Id = r.Id,
                    RoleName = r.Name
                }).ToListAsync();

                return View(users);
            }
            else
            {
                var role = await _roleManager.FindByNameAsync(name);
                var mappedRole = new RoleViewModel()
                {
                    Id = role.Id,
                    RoleName = role.Name
                };
                return View(new List<RoleViewModel>() { mappedRole });
            }
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RoleViewModel model)
        {

            if (ModelState.IsValid)
            {
                var role = _mapper.Map<RoleViewModel, IdentityRole>(model);

                await _roleManager.CreateAsync(role);

                return RedirectToAction(nameof(Index));

            }
            return View(model);
        }

        public async Task<IActionResult> Details(string id, string viewName = "Details")
        {
            if (id is null)
                return BadRequest();

            var role = await _roleManager.FindByIdAsync(id);

            if (role is null)
                return NotFound();

            var mappedRole = _mapper.Map<IdentityRole, RoleViewModel>(role);

            return View(viewName, mappedRole);
        }

        // GET: Employee/Edit/5
        public async Task<IActionResult> Edit(string id)
        {

            return await Details(id, "Edit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, RoleViewModel model)
        {

            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    //var user = _mapper.Map<UserViewModel, ApplicationUser>(model);
                    var role = await _roleManager.FindByIdAsync(id);
                    role.Name = model.RoleName;

                    await _roleManager.UpdateAsync(role);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            return View(model);
        }


        public async Task<IActionResult> Delete(string id)
        {
            return await Details(id, "Delete");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string id, RoleViewModel model)
        {
            if (id != model.Id)
                return BadRequest();
            try
            {
                var role = await _roleManager.FindByIdAsync(id);

                await _roleManager.DeleteAsync(role);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(String.Empty, ex.Message);
            }
            return View(model);
        }


    }
}
