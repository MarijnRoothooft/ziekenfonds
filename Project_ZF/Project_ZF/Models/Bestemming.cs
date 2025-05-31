using System.ComponentModel.DataAnnotations;

namespace Project_ZF.Models
{
    public class Bestemming
    {
        public int Id { get; set; }
        [Required]
        public string Code { get; set; }
        [Required]
        public string BestemmingsNaam { get; set; }
        [Required]
        public string Beschrijving { get; set; }
        public int MinLeeftijd { get; set; }
        public int MaxLeeftijd { get; set; }
        public List<Foto> Fotos { get; set; } = new List<Foto>();
        public List<Groepsreis> Groepsreizen { get; set; } = new List<Groepsreis>();
    }
}
