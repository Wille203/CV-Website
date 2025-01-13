using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CV_Website.ViewModels
{
    public class ChangePasswordViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [DisplayName("Nuvarande lösenord")]
        public string CurrentPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [DisplayName("Nytt lösenord")]
        public string NewPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "Matchar ej ditt nya lösenord.")]
        [DisplayName("Bekräfta lösenord")]
        public string ConfirmPassword { get; set; }
    }
}
