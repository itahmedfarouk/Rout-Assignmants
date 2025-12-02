using System.ComponentModel.DataAnnotations;
using GymCRM.Validation;   // 👈 مهم

namespace GymCRM.ViewModels
{
    public class Step2PersonalViewModel
    {
        [Required(ErrorMessage = "الاسم مطلوب.")]
        [StringLength(100, ErrorMessage = "الاسم طويل جدًا.")]
        public string FullName { get; set; } = "";

        [Required(ErrorMessage = "رقم الهوية مطلوب.")]
        [RegularExpression(@"^(\d{10}|\d{14})$", ErrorMessage = "رقم الهوية يجب أن يكون 10 أو 14 رقمًا.")]
        public string NationalId { get; set; } = "";

        [Required(ErrorMessage = "الجوال مطلوب.")]
        [RegularExpression(@"^\+?\d{9,14}$", ErrorMessage = "رقم الجوال غير صالح.")]
        public string Phone { get; set; } = "";

        [Required(ErrorMessage = "البريد مطلوب.")]
        [EmailAddress(ErrorMessage = "صيغة البريد غير صحيحة.")]
        public string Email { get; set; } = "";

        [Required(ErrorMessage = "اسم المستخدم مطلوب.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "اسم المستخدم يجب أن يكون بين 3 و 50 حرفًا.")]
        [RegularExpression(@"^[A-Za-z0-9_.-]+$", ErrorMessage = "اسم المستخدم يسمح بالحروف والأرقام و _ . - فقط.")]
        public string Username { get; set; } = "";

        [Required(ErrorMessage = "كلمة المرور مطلوبة.")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "كلمة المرور لا تقل عن 6 أحرف.")]
        public string Password { get; set; } = "";

        [Compare(nameof(Password), ErrorMessage = "تأكيد كلمة المرور غير مطابق.")]
        public string ConfirmPassword { get; set; } = "";

        [MustBeTrue]   // ✅ بدل Range
        public bool AcceptTerms { get; set; }
    }
}
