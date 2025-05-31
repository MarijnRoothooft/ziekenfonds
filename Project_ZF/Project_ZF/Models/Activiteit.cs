using System.ComponentModel.DataAnnotations;

namespace Project_ZF.Models
{
    public class Activiteit
    {
        public int Id { get; set; }
        [Required]
        public string Naam { get; set; }

        [Required]
        public string Beschrijving {get; set; }

        public List<Programma> Programmas { get; set; } = default!;
    }
}
