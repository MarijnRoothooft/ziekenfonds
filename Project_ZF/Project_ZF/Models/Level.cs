using System.ComponentModel.DataAnnotations;

namespace Project_ZF.Models
{
    public class Level
    {
        public int Id { get; set; }
        [Required]
        public string Naam { get; set; } 
        public int BenodigdePunten { get; set; }
        public List<CustomUser> Gebruikers { get; set; } = new();
        public List<Beloning> Beloningen { get; set; } = new();
    }
}
