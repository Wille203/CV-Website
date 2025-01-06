using Microsoft.AspNetCore.Identity;


namespace CV_Website.Models
{
    public class User:IdentityUser
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public Boolean Private { get; set; }

        public virtual ICollection<Project> Project { get; set; } = new List<Project>();
        public virtual ICollection<Message> SentMessages { get; set; } = new List<Message>();
        public virtual ICollection<Message> ReceivedMessages { get; set; } = new List<Message>();
    }
}
