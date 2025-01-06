using System.ComponentModel.DataAnnotations;

namespace CV_Website.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Vänligen skriv ett användarnamn.")]
        [StringLength(255)]
        public string AnvandarNamn { get; set; }

        [Required(ErrorMessage = "Vänligen skriv lösenord.")]
        [DataType(DataType.Password)]
        public string Losenord { get; set; }

        [Required(ErrorMessage = "Vänligen bekräfta lösenordet.")]
        [DataType(DataType.Password)]
        [Compare("Losenord", ErrorMessage = "Lösenorden matchar inte.")]
        public string BekraftaLosenord { get; set; }

        [Required(ErrorMessage = "Vänligen skriv ditt namn.")]
        [StringLength(100)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Vänligen ange en giltig e-postadress.")]
        [EmailAddress(ErrorMessage = "Ogiltig e-postadress.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Vänligen skriv din adress.")]
        [StringLength(255)]
        public string Address { get; set; }

        [Display(Name = "Privat konto")]
        public bool Private { get; set; }
    }
}

