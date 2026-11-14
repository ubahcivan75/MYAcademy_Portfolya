using System.ComponentModel.DataAnnotations;

namespace MYAcademy_Portfolya.Entites
{
    public class Message
    {
        public int MessageId { get; set; }

        [Required(ErrorMessage = "Ad soyad boş geçilemez.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Ad soyad 3-50 karakter arasında olmalı.")]
        public string NameSurname { get; set; }

        [Required(ErrorMessage = "E-posta boş geçilemez.")]
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi girin.")]
        [StringLength(100, ErrorMessage = "E-posta en fazla 100 karakter olabilir.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Konu boş geçilemez.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Konu 3-100 karakter arasında olmalı.")]
        public string Subject { get; set; }

        [Required(ErrorMessage = "Mesaj boş geçilemez.")]
        [StringLength(2000, MinimumLength = 10, ErrorMessage = "Mesaj 10-2000 karakter arasında olmalı.")]
        public string MessageDetail { get; set; }

        public bool IsRead { get; set; }          // okundu mu?
        public DateTime SendDate { get; set; }    // gönderilme tarihi
    }
}