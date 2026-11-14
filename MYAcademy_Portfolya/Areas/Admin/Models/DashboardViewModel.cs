using MYAcademy_Portfolya.Entites;

namespace MYAcademy_Portfolya.Areas.Admin.Models
{
    public class DashboardViewModel
    {
        // İstatistik kartları
        public int ProjectCount { get; set; }
        public int ActiveProjectCount { get; set; }
        public int SkillCount { get; set; }
        public int ExperienceCount { get; set; }
        public int EducationCount { get; set; }
        public int MessageCount { get; set; }
        public int UnreadMessageCount { get; set; }

        // Listeler
        public List<Message> LastMessages { get; set; } = new();
        public List<Project> LastProjects { get; set; } = new();
        public List<CategorySummary> CategorySummaries { get; set; } = new();
    }

    // Kategori özeti için küçük yardımcı sınıf
    public class CategorySummary
    {
        public string CategoryName { get; set; } = string.Empty;
        public int ProjectCount { get; set; }
        public int ActiveProjectCount { get; set; }
    }
}