using System.ComponentModel.DataAnnotations;

namespace MYAcademy_Portfolya.Entites
{
    public class HeroSection
    {
        public int HeroSectionId { get; set; }

        [Required(ErrorMessage = "Başlık boş geçilemez.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "İsim boş geçilemez.")]
        public string SubTitle { get; set; }

        [Required(ErrorMessage = "Açıklama boş geçilemez.")]
        public string Description { get; set; }

        public string ImageUrl { get; set; }
    }
}