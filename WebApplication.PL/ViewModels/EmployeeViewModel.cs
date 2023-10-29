using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApplicaiton.DAL.Models;

namespace WebApplication.PL.ViewModels
{
    public class EmployeeViewModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(55)]
        public string Name { get; set; }

        public int? Age { get; set; }

        public string Address { get; set; }

        public decimal Salary { get; set; }

        public bool IsActive { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public DateTime HireDate { get; set; }

        [ForeignKey("Department")]
        public int? DepartmentId { get; set; }

        public Department Department { get; set; }

        public IFormFile Image { get; set; }

        public string ImageName { get; set; }
    }
}
