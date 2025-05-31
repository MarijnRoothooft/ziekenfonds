using System.ComponentModel.DataAnnotations;

namespace Project_ZF.Models
{
    public class Punten
    {
        public int Id { get; set; }
        public int DeelnemerId { get; set; }
        public int GroepsreisId { get; set; }
        public int AantalPunten { get; set; }
        [DataType(DataType.Date)]
        public DateTime Datum { get; set; }
        [Required]
        public string Omschrijving { get; set; }

        public Deelnemer Deelnemer { get; set; }
        public Groepsreis Groepsreis { get; set; }
    }
}
