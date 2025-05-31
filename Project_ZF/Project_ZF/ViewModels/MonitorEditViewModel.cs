using System.ComponentModel.DataAnnotations;

namespace Project_ZF.ViewModels
{
    public class MonitorEditViewModel
    {

        [Required(ErrorMessage = "Vul een Familienaam in aub.")]
        [RegularExpression(@"^[a-zA-Z\s]*$", ErrorMessage = "Alleen letters en spaties zijn toegestaan.")]
        public string Naam { get; set; }

        [Required(ErrorMessage = "Vul een Voornaam in aub.")]
        [RegularExpression(@"^[a-zA-Z\s]*$", ErrorMessage = "Alleen letters en spaties zijn toegestaan.")]
        public string Voornaam { get; set; }

        [Required(ErrorMessage = "Vul een Email in aub.")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Vul een geldig e-mailadres in aub.")]
        public string Email { get; set; }
        public bool IsActief { get; set; }

        public bool IsHoofdMonitor { get; set; }    
    }
}
