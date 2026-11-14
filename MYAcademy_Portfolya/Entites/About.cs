using System.ComponentModel.DataAnnotations;

namespace MYAcademy_Portfolya.Entites
{
    public class About
    {
        public int AboutId { get; set; }

        [Required(ErrorMessage = "Ad soyad boş geçilemez.")]
        public string NameSurName { get; set; }

        [Required(ErrorMessage = "Meslek boş geçilemez.")]
        public string JobTitle { get; set; }

        [Required(ErrorMessage = "Lokasyon boş geçilemez.")]
        public string Location { get; set; }

        [Required(ErrorMessage = "Uzmanlık alanı boş geçilemez.")]
        public string Expert { get; set; }

        [Required(ErrorMessage = "E-posta boş geçilemez.")]
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi girin.")]
        public string Mail { get; set; }

        [Required(ErrorMessage = "Hakkımda yazısı boş geçilemez.")]
        [MinLength(20, ErrorMessage = "Hakkımda yazısı en az 20 karakter olmalı.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Çalışma durumu seçilmelidir.")]
        public string OpenToWork { get; set; }
    }
}