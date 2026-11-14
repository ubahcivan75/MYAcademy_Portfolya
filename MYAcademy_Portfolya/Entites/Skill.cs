using System.ComponentModel.DataAnnotations;

namespace MYAcademy_Portfolya.Entites
{
    public class Skill
    {
        public int SkillId { get; set; }

        [Required(ErrorMessage = "Yetenek adı boş geçilemez.")]
        public string Name { get; set; } = string.Empty;   // C#

        [Range(5, 100, ErrorMessage = "Yetkinlik seviyesi %5 ile %100 arasında olmalı.")]
        public int Percentage { get; set; }                 // 85

        // İlişki: her yetenek bir kategoriye ait
        public int SkillCategoryId { get; set; }
        public SkillCategory? SkillCategory { get; set; }
    }
}