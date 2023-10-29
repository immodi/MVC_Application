using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplicaiton.DAL.Models;
using WebApplication.PL.ViewModels;

namespace WebApplication.PL.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IMapper _mapper;

        public UserController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
        }

        // GET: Employee
        public async Task<IActionResult> Index(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                var users = _userManager.Users.Select(u => new UserViewModel()
                {
                    Id = u.Id,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Email = u.Email,
                    PhoneNumber = u.PhoneNumber,
                    Roles = _userManager.GetRolesAsync(u).Result
                }).ToListAsync();
                return View(await users);
            }
            else
            {
                var user = await _userManager.FindByEmailAsync(email);
                var mappedUser = new UserViewModel()
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    Roles = _userManager.GetRolesAsync(user).Result
                };
                return View(new List<UserViewModel>() { mappedUser });
            }
        }

        // GET: Employee/Details/5
        public async Task<IActionResult> Details(string id, string viewName = "Details")
        {
            if (id is null)
                return BadRequest();

            var user = await _userManager.FindByIdAsync(id);

            if (user is null)
                return NotFound();

            var mappedUser = _mapper.Map<ApplicationUser, UserViewModel>(user);

            return View(viewName, mappedUser);
        }

        // GET: Employee/Edit/5
        public async Task<IActionResult> Edit(string id)
        {

            return await Details(id, "Edit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, UserViewModel model)
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
                    var user = await _userManager.FindByIdAsync(id);
                    user.FirstName = model.FirstName;
                    user.LastName = model.LastName;
                    user.PhoneNumber = model.PhoneNumber;

                    await _userManager.UpdateAsync(user);
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
        public async Task<IActionResult> Delete(string id, UserViewModel model)
        {
            if (id != model.Id)
                return BadRequest();
            try
            {
                var user = await _userManager.FindByIdAsync(id);

                await _userManager.DeleteAsync(user);
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
