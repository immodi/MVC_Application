using System.ComponentModel.DataAnnotations;

namespace WebApplication.PL.ViewModels
{
    public class ResetPasswordViewModel
    {

        [Required(ErrorMessage = "New Password is Required")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Password is Required")]
        [Compare("NewPassword", ErrorMessage = "Confirmed Password Doesn't Match The Original Password")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
