using System.ComponentModel.DataAnnotations;

namespace Project_ZF.ViewModels
{
    public class BeloningWijzigenViewModel
    {
            public int Id { get; set; }

            [Required(ErrorMessage = "Vul een naam voor de beloning in aub.")]
            [RegularExpression(@"^[a-zA-Z0-9\s]*$", ErrorMessage = "Alleen letters, cijfers en spaties zijn toegestaan.")]
            public string Naam { get; set; }

            [Required(ErrorMessage = "Vul een beschrijving in aub.")]
            [RegularExpression(@"^[a-zA-Z0-9\s]*$", ErrorMessage = "Alleen letters, cijfers en spaties zijn toegestaan.")]
            public string Beschrijving { get; set; }
            [Required(ErrorMessage = "Selecteer een level.")]
            public int LevelId { get; set; }
            public List<Level> Levels { get; set; } = new List<Level>();
        
    }
}
