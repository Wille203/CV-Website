namespace CV_Website.Models
{
    public class Project
    {
        public int ProjectId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public int CreatorId { get; set; }
        public virtual ICollection<User> User { get; set; } = new List<User>();
    }
}
