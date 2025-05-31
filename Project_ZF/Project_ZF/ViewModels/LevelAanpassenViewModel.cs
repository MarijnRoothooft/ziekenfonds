using System.ComponentModel.DataAnnotations;

namespace Project_ZF.ViewModels
{
    public class LevelAanpassenViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Vul een naam voor de beloning in aub.")]
        [RegularExpression(@"^[a-zA-Z\s]*$", ErrorMessage = "Alleen letters en spaties zijn toegestaan.")]
        public string Naam { get; set; }

        [Required(ErrorMessage = "Vul het aantal benodigde punten in aub.")]
        [Range(0, int.MaxValue, ErrorMessage = "Alleen positieve getallen en nul zijn toegestaan.")]
        public int BenodigdePunten { get; set; }
    }
}
