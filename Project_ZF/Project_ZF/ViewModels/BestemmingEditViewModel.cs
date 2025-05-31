using System.ComponentModel.DataAnnotations;

namespace Project_ZF.ViewModels
{
    public class BestemmingEditViewModel
    {
        public int Id { get; set; }

        [RegularExpression(@"^[a-zA-Z]*$", ErrorMessage = "Enkel letters zijn toegelaten.")]
        public string Code { get; set; }

        [RegularExpression(@"^[a-zA-Z\s]*$", ErrorMessage = "Alleen letters en spaties zijn toegestaan.")]
        public string BestemmingsNaam { get; set; }

        public string Beschrijving { get; set; }

        [RegularExpression(@"^\d+$", ErrorMessage = "Enkel getallen zijn toegelaten.")]
        public int MinLeeftijd { get; set; }

        [RegularExpression(@"^\d+$", ErrorMessage = "Enkel getallen zijn toegelaten.")]
        public int MaxLeeftijd { get; set; }

        // Lijst van foto's die bij de bestemming horen.
        public List<FotoViewModel> Fotos { get; set; } = new List<FotoViewModel>(); // Initialiseer de lijst
        public List<FotoViewModel> FotosToRemove { get; set; } = new List<FotoViewModel>(); // Initialiseer de lijst

        // Bestand voor het uploaden van een foto.
        public IFormFile? Foto { get; set; } // de foto is optioneel
    }
}