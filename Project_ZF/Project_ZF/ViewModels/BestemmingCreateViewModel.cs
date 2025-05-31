using System.ComponentModel.DataAnnotations;

namespace Project_ZF.ViewModels
{
    public class BestemmingCreateViewModel
    {
        [Required]
        [RegularExpression(@"^[a-zA-Z]*$", ErrorMessage = "Enkel letters zijn toegelaten.")]
        public string Code { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z]*$", ErrorMessage = "Enkel letters zijn toegelaten.")]
        public string BestemmingsNaam { get; set; }

        [Required]
        public string Beschrijving { get; set; }

        [Required]
        [RegularExpression(@"^\d+$", ErrorMessage = "Enkel getallen zijn toegelaten.")]
        public int MinLeeftijd { get; set; }

        [Required]
        [RegularExpression(@"^\d+$", ErrorMessage = "Enkel getallen zijn toegelaten.")]
        public int MaxLeeftijd { get; set; }
        // Bestand voor het uploaden van een foto.
        public IFormFile Foto { get; set; }
    }
}
