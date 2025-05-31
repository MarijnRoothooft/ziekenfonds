using System.ComponentModel.DataAnnotations;

namespace Project_ZF.ViewModels
{
    public class KindToevoegenViewModel
    {
        [Required(ErrorMessage = "Vul een Familienaam in aub.")]
        [RegularExpression(@"^[a-zA-Z\s]*$", ErrorMessage = "Alleen letters en spaties zijn toegestaan.")]
        public string Naam { get; set; }

        [Required(ErrorMessage = "Vul een Voornaam in aub.")]
        [RegularExpression(@"^[a-zA-Z\s]*$", ErrorMessage = "Alleen letters en spaties zijn toegestaan.")]
        public string Voornaam { get; set; }

        [Required(ErrorMessage = "Geboortedatum is verplicht.")]
        [DataType(DataType.Date, ErrorMessage = "Ongeldige datum.")]
        [Geboortedatum(ErrorMessage = "Geboortedatum mag niet in de toekomst liggen.")]
        public DateTime Geboortedatum { get; set; }

        [Required(ErrorMessage = "Vul in of je kind allergieën heeft aub.")]
        public string Allergieën { get; set; }

        [Required(ErrorMessage = "Vul in of je kind medicatie heeft aub.")]
        public string Medicatie { get; set; }
    }

   
}
