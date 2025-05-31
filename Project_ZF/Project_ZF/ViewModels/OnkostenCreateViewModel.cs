using System.ComponentModel.DataAnnotations;

namespace Project_ZF.ViewModels
{
    public class OnkostenCreateViewModel
    {
        [Required(ErrorMessage = "Bestemming is verplicht.")]
        public int GroepsreisId { get; set; }

        [Required(ErrorMessage = "Titel is verplicht.")]
        public string Titel { get; set; }

        [Required(ErrorMessage = "Beschrijving is verplicht.")]
        public string Omschrijving { get; set; }

        [Required(ErrorMessage = "Bedrag is verplicht.")]
        [Range(0, 10000.99, ErrorMessage = "Voer een geldig bedrag in tussen 0 en 10.000.")]
        public double Bedrag { get; set; }

        [Required(ErrorMessage = "De datum is verplicht.")]
        [DataType(DataType.Date)]
        public DateTime Datum { get; set; }

        [RegularExpression(@"^.*\.(jpg|jpeg|png|gif)$", ErrorMessage = "Alleen afbeeldingsbestanden zijn toegestaan.")]
        public IFormFile? Foto { get; set; }
    }


}
