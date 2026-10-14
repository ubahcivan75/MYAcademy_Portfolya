using System.Globalization;

namespace MYAcademy_Portfolya.Entites
{
    public class Experience
    {
        public int ExperienceId { get; set; }
        public string Title { get; set; }
        public string CompanyName { get; set; }
        public string Description { get; set; }
        public String SideTitle { get; set; }
        public string Duration { get; set; }
        public string WorkType { get; set; }
        public bool IsActive { get; set; }

    }
}
