using System.ComponentModel.DataAnnotations;

namespace CV_Website.ViewModels
{
    public class ChangePasswordViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        public string CurrentPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "Matchar ej ditt nya lösenord.")]
        public string ConfirmPassword { get; set; }
    }
}
