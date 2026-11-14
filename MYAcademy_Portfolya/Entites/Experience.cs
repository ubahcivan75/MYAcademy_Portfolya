using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;

namespace MYAcademy_Portfolya.Entites
{
    public class Experience
    {
        public int ExperienceId { get; set; }

        [Required(ErrorMessage = "Pozisyon / ünvan boş geçilemez.")]
        [StringLength(100, ErrorMessage = "En fazla 100 karakter olabilir.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Şirket adı boş geçilemez.")]
        [StringLength(100, ErrorMessage = "En fazla 100 karakter olabilir.")]
        public string CompanyName { get; set; }

        [Required(ErrorMessage = "Açıklama boş geçilemez.")]
        [StringLength(2000, ErrorMessage = "Açıklama en fazla 2000 karakter olabilir.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Alt başlık boş geçilemez.")]
        [StringLength(100, ErrorMessage = "En fazla 100 karakter olabilir.")]
        public string SideTitle { get; set; }

        [Required(ErrorMessage = "Çalışma tipi seçilmelidir.")]
        [StringLength(30)]
        public string WorkType { get; set; }

        [Required(ErrorMessage = "Başlangıç tarihi seçilmelidir.")]
        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }      // boş = halen çalışıyor

        public bool IsActive { get; set; }          // true = sitede görünür

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