using System.ComponentModel.DataAnnotations;

namespace MYAcademy_Portfolya.Entites
{
    public class Project
    {
        public int ProjectId { get; set; }

        [Required(ErrorMessage = "Proje başlığı boş geçilemez.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Açıklama boş geçilemez.")]
        [MinLength(20, ErrorMessage = "Açıklama en az 20 karakter olmalı.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "GitHub linki boş geçilemez.")]
        [Url(ErrorMessage = "Geçerli bir link girin. (https:// ile başlamalı)")]
        public string GithubUrl { get; set; }

        public string CoverImageUrl { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Lütfen bir kategori seçin.")]
        public int CategoryId { get; set; }

        public Category Category { get; set; }
        public bool IsActive { get; set; }   // true = sitede yayında, false = gizli
    }
}
