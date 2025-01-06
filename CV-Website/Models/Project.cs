using System.ComponentModel.DataAnnotations.Schema;

namespace CV_Website.Models
{
    public class Project
    {
        public int ProjectId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string? Information { get; set; }
        public int CreatorId { get; set; }
        [ForeignKey(nameof(CreatorId))]
        public virtual User Creator { get; set; }

        public virtual ICollection<User> Users { get; set; } = new List<User>();
        
    }
}
