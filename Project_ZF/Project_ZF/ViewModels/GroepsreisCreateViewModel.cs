using System.ComponentModel.DataAnnotations;

namespace Project_ZF.ViewModels
{
    public class GroepsreisCreateViewModel
    {
        public int GroepsreisId { get; set; } // Id van de bestemming
        public string BestemmingsNaam { get; set; } = string.Empty; // Naam van de bestemming

        [Required]
        [DataType(DataType.Date)]
        public DateTime BeginDatum { get; set; } // Begin datum van de groepsreis

        [Required]
        [DataType(DataType.Date)]
        public DateTime EindDatum { get; set; } // Eind datum van de groepsreis

  
        [Required(ErrorMessage = "Vul De prijs in aub.")]
        [Range(0, int.MaxValue, ErrorMessage = "Alleen positieve getallen en nul zijn toegestaan.")]
        public double Prijs { get; set; } // Prijs van de groepsreis

        [Required(ErrorMessage = "Vul het aantal benodigde punten in aub.")]
        [Range(0, int.MaxValue, ErrorMessage = "Alleen positieve getallen en nul zijn toegestaan.")]
        public int StandaardPunten { get; set; } 
    }
}
