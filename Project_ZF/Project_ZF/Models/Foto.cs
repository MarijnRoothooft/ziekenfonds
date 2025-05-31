using System.ComponentModel.DataAnnotations;

namespace Project_ZF.Models
{
    public class Foto
    {
        public int Id { get; set; }

        [Required]
        public string Naam { get; set; }

        public int BestemmingId { get; set; }

        public Bestemming Bestemming { get; set; } = default!;
    }
}
