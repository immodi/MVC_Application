using System.ComponentModel.DataAnnotations;

namespace WebApplication.PL.ViewModels
{
    public class ForgetPasswordViewModel
    {
        [Required(ErrorMessage = "Email is Required")]
        [EmailAddress(ErrorMessage = "Email is Invaild")]
        public string Email { get; set; }


    }
}
