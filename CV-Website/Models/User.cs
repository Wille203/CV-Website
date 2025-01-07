using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CV_Website.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Name { get; set; }

        [Required(ErrorMessage = "Du kan inte logga in utan ett lösenord")]
        public string Password { get; set; }

        [StringLength(50, ErrorMessage = "Ange en giltig adress")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Du måste ange en e-postadress")]
        public string Email { get; set; }
        public Boolean Private { get; set; }

        public virtual ICollection<Project> Project { get; set; } = new List<Project>();
        public virtual ICollection<Message> SentMessages { get; set; } = new List<Message>();
        public virtual ICollection<Message> ReceivedMessages { get; set; } = new List<Message>();
    }
}
