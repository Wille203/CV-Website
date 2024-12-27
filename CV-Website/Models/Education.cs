namespace CV_Website.Models
{
    public class Education
    {
        public int EducationId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<CV> CV { get; set; } = new List<CV>();
    }
}
