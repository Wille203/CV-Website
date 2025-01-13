using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CV_Website.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [DisplayName("Namn")]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        [DisplayName("Email")]
        public string UserName { get; set; }

        [Required]
        [Phone]
        [DisplayName("Telefon")]
        public string PhoneNumber { get; set; }

        public string Address { get; set; }

        [Required]
        [DisplayName("Lösenord")]
        public string Password { get; set; }

        [Required]
        [Compare("Password")]
        [DisplayName("Bekräfta Lösenord")]
        public string ConfirmPassword { get; set; }

        [Required]
        [DisplayName("Privat")]
        public bool Private { get; set; }
    }

}
