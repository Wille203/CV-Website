namespace CV_Website.Models
{
    public class Experience
    {
        public string ExperienceId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<CV> CV { get; set; } = new List<CV>();
    }
}
