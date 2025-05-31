using System.ComponentModel.DataAnnotations;

namespace Project_ZF.ViewModels
{
    public class GroepsreisEditViewModel
    {
        public int GroepsreisId { get; set; } // Id van de bestemming
        public string BestemmingsNaam { get; set; } = string.Empty; // Naam van de bestemming
        public int Id { get; set; }
        public DateTime BeginDatum { get; set; }
        public DateTime EindDatum { get; set; }
        [Required(ErrorMessage = "Vul de Prijs in aub.")]
        [Range(0, int.MaxValue, ErrorMessage = "Alleen positieve getallen en nul zijn toegestaan.")]
        public double Prijs { get; set; }
        [Required(ErrorMessage = "Vul het aantal benodigde punten in aub.")]
        [Range(0, int.MaxValue, ErrorMessage = "Alleen positieve getallen en nul zijn toegestaan.")]
        public int StandaardPunten { get; set; }
    }
}