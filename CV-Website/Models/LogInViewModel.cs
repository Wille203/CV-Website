
using System.ComponentModel.DataAnnotations;

namespace CV_Website.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Vänligen skriv en UserName.")]
        [StringLength(255)]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Vänligen skriv ett lösenord.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}




