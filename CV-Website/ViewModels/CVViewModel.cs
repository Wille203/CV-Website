using CV_Website.Models;

namespace CV_Website.ViewModels
{
    public class CVViewModel
    {
        public int UserId { get; set; }
        public List<Skills> AllSkills { get; set; } = new List<Skills>();
        public List<Education> AllEducations { get; set; } = new List<Education>();
        public List<Experience> AllExperiences { get; set; } = new List<Experience>();

        // Användarens nuvarande val 
        public List<int> SelectedSkills { get; set; } = new List<int>();  
        public List<int> SelectedEducations { get; set; } = new List<int>();  
        public List<int> SelectedExperiences { get; set; } = new List<int>();  
    }
}
