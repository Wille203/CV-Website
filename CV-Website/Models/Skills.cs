namespace CV_Website.Models
{
    public class Skills
    {
        public int SkillsId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<CV> CV { get; set; } = new List<CV>();
    }
}
