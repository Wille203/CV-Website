using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.SqlServer.Server;

namespace CV_Website.Models
{
    public class User
    {
        public int UserId { get; set; }

        [Required(ErrorMessage = "Ange ditt förnamn")]
        [RegularExpression("^[A-Za-z]+$", ErrorMessage = "Hörru, ditt namn består av bokstäver inget annat")]
        public string Name { get; set; }

        [StringLength(25, ErrorMessage = "Ditt lösenord får inte vara längre än 25 tecken")]
        [Required(ErrorMessage = "Du kan inte logga in utan ett lösenord")]
        public string Password { get; set; }


        [StringLength(50, ErrorMessage = "Ange en giltig adress")]
        [RegularExpression("^[A-Za-z0-9]+$", ErrorMessage = "Bokstäver och siffror är tillåtet, inget annat")]
        public string Address { get; set; }

        //@ gör det mer lättläst exempelvis behövs inte \\, E-post måste innehålle @, punkt, samt en TDL
        [RegularExpression(@"^[a-zA-Z0-9._+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "E-postadressen måste vara i korrekt format, till exempel: exempel@epost.com.")]
        [Required(ErrorMessage = "Du måste ange en e-postadress")]
        public string Email { get; set; }

        [RegularExpression(@"^\+?[0-9]{7,15}$", ErrorMessage = "Ange ett giltigt telefonnummer (7–15 siffror, valfritt + i början).")]
        [StringLength(15, ErrorMessage = "Telefonnumret får inte vara längre än 15 tecken.")]
        public string PhoneNumber { get; set; }

        public Boolean Private { get; set; }

        public virtual ICollection<Project> Project { get; set; } = new List<Project>();
        public virtual ICollection<Message> SentMessages { get; set; } = new List<Message>();
        public virtual ICollection<Message> ReceivedMessages { get; set; } = new List<Message>();
    }
}
