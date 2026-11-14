using System.ComponentModel.DataAnnotations;

namespace MYAcademy_Portfolya.Entites
{
    public class SkillCategory
    {
        public int SkillCategoryId { get; set; }

        [Required(ErrorMessage = "Kategori adı boş geçilemez.")]
        public string Title { get; set; } = string.Empty;        // Backend Development

        [Required(ErrorMessage = "Açıklama boş geçilemez.")]
        public string Description { get; set; } = string.Empty;  // Kısa açıklama

        [Required(ErrorMessage = "İkon boş geçilemez.")]
        [RegularExpression(@"^bi-[a-z0-9-]+$", ErrorMessage = "İkon 'bi-' ile başlamalı. Örn: bi-database")]
        public string Icon { get; set; } = string.Empty;         // bi-hdd-network

        [Required(ErrorMessage = "Renk seçilmelidir.")]
        public string Color { get; set; } = string.Empty;        // bg-icon-purple

        [Range(0, 100, ErrorMessage = "Sıra 0 ile 100 arasında olmalı.")]
        public int DisplayOrder { get; set; }                    // Sitedeki sırası

        // Bu kategoriye ait yetenekler (bire-çok ilişkinin "çok" tarafı)
        public List<Skill> Skills { get; set; } = new();
    }
}