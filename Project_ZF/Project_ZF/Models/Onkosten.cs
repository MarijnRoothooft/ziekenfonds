using System.ComponentModel.DataAnnotations;
using System.Globalization; 

namespace Project_ZF.Models
{
    public class Onkosten
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Bestemming is verplicht.")]
        [Display(Name = "Bestemming")]
        public int GroepsreisId { get; set; }

        [Required(ErrorMessage = "De titel is verplicht.")]
        public string Titel { get; set; }

        [Required(ErrorMessage = "Beschrijving is verplicht.")]
        public string Omschrijving { get; set; }

        [Required(ErrorMessage = "Bedrag is verplicht.")]
        [Range(0, 10000.99, ErrorMessage = "Voer een geldig bedrag in tussen 0 en 10.000.")]
        [DisplayFormat(DataFormatString = "{0:F2}", ApplyFormatInEditMode = true)]
        public double Bedrag { get; set; }

        [Required(ErrorMessage = "De datum is verplicht.")]
        [DataType(DataType.Date)]
        public DateTime Datum { get; set; }

        [RegularExpression(@"^.*\.(jpg|jpeg|png|gif)$", ErrorMessage = "Alleen afbeeldingsbestanden zijn toegestaan.")]
        public string? Foto { get; set; }

        // Navigatie-eigenschap (niet verplicht in de API request)
        public Groepsreis? Groepsreis { get; set; }
    }
}
