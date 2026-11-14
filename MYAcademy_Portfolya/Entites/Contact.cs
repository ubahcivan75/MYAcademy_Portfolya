using System.ComponentModel.DataAnnotations;

namespace MYAcademy_Portfolya.Entites
{
    public class Contact
    {
        public int ContactId { get; set; }

        [Required(ErrorMessage = "Açıklama boş geçilemez.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "E-posta boş geçilemez.")]
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi girin.")]
        public string Emil { get; set; }

        [Required(ErrorMessage = "LinkedIn linki boş geçilemez.")]
        [Url(ErrorMessage = "Geçerli bir link girin. (https:// ile başlamalı)")]
        public string Linkein { get; set; }

        [Required(ErrorMessage = "Konum boş geçilemez.")]
        public string Location { get; set; }

        [Required(ErrorMessage = "GitHub linki boş geçilemez.")]
        [Url(ErrorMessage = "Geçerli bir link girin. (https:// ile başlamalı)")]
        public string Github { get; set; }

        [Required(ErrorMessage = "Alt açıklama boş geçilemez.")]
        public string SubDescription { get; set; }
    }
}