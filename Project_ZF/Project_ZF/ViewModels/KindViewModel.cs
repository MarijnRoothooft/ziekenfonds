using System.ComponentModel.DataAnnotations;

namespace Project_ZF.ViewModels
{
    public class KindViewModel
    {
        public int Id { get; set; }
        public string PersoonId { get; set; }
        [Required]
        public string Naam { get; set; }
        [Required]
        public string Voornaam { get; set; }

        [DataType(DataType.Date)]
        public DateTime Geboortedatum { get; set; }
        [Required]
        public string Allergieën { get; set; }
        [Required]
        public string Medicatie { get; set; }
        public CustomUser CustomUser { get; set; }
    }
}
