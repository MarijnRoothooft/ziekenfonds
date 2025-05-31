using System.ComponentModel.DataAnnotations;

namespace Project_ZF.ViewModels
{
    public class KindWijzigenViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Familienaam?.")]
        [RegularExpression(@"^[a-zA-Z\s]*$", ErrorMessage = "Alleen letters en spaties zijn toegestaan.")]
        public string Naam { get; set; }

        [Required(ErrorMessage = "Voornaam?.")]
        [RegularExpression(@"^[a-zA-Z]*$", ErrorMessage = "Alleen letters zijn toegestaan.")]
        public string Voornaam { get; set; }

        [Required(ErrorMessage = "Geboortedatum?.")]
        [DataType(DataType.Date, ErrorMessage = "Ongeldige datum.")]
        [Geboortedatum(ErrorMessage = "Geboortedatum mag niet in de toekomst liggen.")]
        public DateTime Geboortedatum { get; set; }

        [Required(ErrorMessage = "Vul in of je kind allergieën heeft aub.")]
        public string Allergieën { get; set; }

        [Required(ErrorMessage = "Vul in of je kind medicatie heeft aub.")]
        public string Medicatie { get; set; }
    }

     public class GeboortedatumAttribute : ValidationAttribute
     {
        public override bool IsValid(object value)
        {
            if (value is DateTime geboortedatum)
            {
                return geboortedatum <= DateTime.Today;
            }
            return false;
        }
     }

}