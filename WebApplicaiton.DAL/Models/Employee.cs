using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplicaiton.DAL.Models
{
    public class Employee
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Name is Required!")]
        [MaxLength(55, ErrorMessage = "Max Length is 55 characters!")]
        [MinLength(5, ErrorMessage = "Min Length is 5 characters!")]
        public string Name { get; set; }

        [Range(22, 40, ErrorMessage = "Age must be between 22 to 40 years!")]
        public int? Age { get; set; }

        //[RegularExpression("^[0-9]{1-3}-[a-zA-Z]{5-10}-[a-zA-Z]{4,10}-[a-zA-Z]{5-10}$",
        //    ErrorMessage = "Address must be like '123-street-city-country'")]
        public string Address { get; set; }

        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }

        public bool IsActive { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        public string PhoneNumber { get; set; }

        public DateTime HireDate { get; set; }

        public DateTime DateOfCreation { get; set; } = DateTime.Now;

        [ForeignKey("Department")]
        public int? DepartmentId { get; set; }
        public Department Department { get; set; }

        public string ImageName { get; set; }
    }
}
