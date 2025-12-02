using System.ComponentModel.DataAnnotations;

namespace GymCRM.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "اسم المستخدم أو الإيميل مطلوب.")]
        public string UserNameOrEmail { get; set; } = "";

        [Required(ErrorMessage = "كلمة المرور مطلوبة.")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = "";

        public bool RememberMe { get; set; }

        public string? ReturnUrl { get; set; }
    }
}
