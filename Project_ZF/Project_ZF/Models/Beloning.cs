using System.ComponentModel.DataAnnotations;

namespace Project_ZF.Models
{
    public class Beloning
    {
        public int Id { get; set; }
        [Required]
        public string Naam { get; set; }
        [Required]
        public string Beschrijving { get; set; }
        public int LevelId { get; set; }
        public Level Level { get; set; } = default!;
    }
}
