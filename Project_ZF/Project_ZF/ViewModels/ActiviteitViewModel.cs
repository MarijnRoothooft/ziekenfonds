using System.ComponentModel.DataAnnotations;

namespace Project_ZF.ViewModels
{
    public class ActiviteitViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Naam { get; set; }

        [Required]
        public string Beschrijving { get; set; }

        public List<Activiteit> Activiteiten { get; set; } = new List<Activiteit>();
    }
}