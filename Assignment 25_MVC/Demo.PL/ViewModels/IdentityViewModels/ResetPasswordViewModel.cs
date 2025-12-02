
using System.ComponentModel.DataAnnotations;

namespace Demo.PL.ViewModels.IdentityViewModels
{
    public class ResetPasswordViewModel
    {
        [Required(ErrorMessage = "Enter Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = "Enter Confirmed Password")]
        [Compare("Password", ErrorMessage = "Password Not Match")]
        [DataType(DataType.Password)]

        public string ConfirmPassword { get; set; }

    }
}
