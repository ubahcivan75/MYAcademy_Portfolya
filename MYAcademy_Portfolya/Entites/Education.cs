using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;

namespace MYAcademy_Portfolya.Entites
{
    public class Education
    {
        public int EducationId { get; set; }

        [Required(ErrorMessage = "Okul / kurum adı boş geçilemez.")]
        [StringLength(100, ErrorMessage = "En fazla 100 karakter olabilir.")]
        public string Title { get; set; }

        [StringLength(100, ErrorMessage = "En fazla 100 karakter olabilir.")]
        public string? SubTitle { get; set; }

        [Required(ErrorMessage = "Açıklama boş geçilemez.")]
        [StringLength(1000, ErrorMessage = "Açıklama en fazla 1000 karakter olabilir.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Başlangıç tarihi seçilmelidir.")]
        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }      // boş = devam ediyor

        public bool IsActive { get; set; }          // true = sitede görünür

        // Veritabanına yazılmaz; ekranda gösterilecek metni tarihlerden üretir
        [NotMapped]
        public string Period
        {
            get
            {
                if (StartDate == null) return "";
                var tr = new CultureInfo("tr-TR");
                var start = StartDate.Value.ToString("MMMM yyyy", tr);
                var end = EndDate == null ? "Devam ediyor" : EndDate.Value.ToString("MMMM yyyy", tr);
                return $"{start} – {end}";
            }
        }
    }
}
