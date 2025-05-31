using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Project_ZF.ViewModels
{
    public class OpleidingViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Naam is verplicht.")]
        [RegularExpression(@"^[a-zA-Z\s]*$", ErrorMessage = "Alleen letters en spaties zijn toegestaan.")]
        public string Naam { get; set; }

        [Required(ErrorMessage = "Beschrijving is verplicht.")]
        public string Beschrijving { get; set; }

        [Required(ErrorMessage = "De datum is verplicht.")]
        [DataType(DataType.Date)]
        public DateTime BeginDatum { get; set; }

        [Required(ErrorMessage = "De datum is verplicht.")]
        [DataType(DataType.Date)]
        public DateTime EindDatum { get; set; }

        [Required(ErrorMessage = "Alleen getallen zijn toegestaan.")]
        [Range(0, 150, ErrorMessage = "De waarde moet tussen 0 en 150 liggen.")]
        public int AantalPlaatsen { get; set; }

        public int? OpleidingVereistId { get; set; }
      
        // Voeg de naam van de vereiste opleiding toe
        public string VereisteOpleidingNaam { get; set; }
      
        public List<Opleiding> VereisteOpleidingen { get; set; } = new List<Opleiding>();
        [Required]
        [Display(Name = "aangeduidde Opleiding")]
        public Opleiding AangeduiddeOpleiding { get; set; }

        // This property will hold all available states for selection
        public IEnumerable<SelectListItem> OpleidingenAanduiden { get; set; }
    }
}