using System.ComponentModel.DataAnnotations;

namespace Project_ZF.Models
{
    public class Groepsreis
    {
        public int Id { get; set; } // Id van de groepsreis
        public int GroepsreisId { get; set; } // Id van de bestemming

        [DataType(DataType.Date)]
        public DateTime BeginDatum { get; set; } // Begin datum van de groepsreis

        [DataType(DataType.Date)]
        public DateTime EindDatum { get; set; } // Eind datum van de groepsreis
        public double Prijs { get; set; } // Prijs van de groepsreis

        public int StandaardPunten { get; set; }
        public Bestemming Bestemming { get; set; } = default!; // Bestemming van de groepsreis
        public List<Onkosten> Onkosten { get; set; } = new List<Onkosten>(); // Lijst van onkosten
        public List<Programma> Programmas { get; set; } = new List<Programma>(); // Lijst van programma's
        public List<Deelnemer> Deelnemers { get; set; } = new List<Deelnemer>(); // Lijst van deelnemers
        public List<Monitor> Monitoren { get; set; } = new List<Monitor>(); // Lijst van monitoren
        public List<Punten> Punten { get; set; } = new List<Punten>();
    }
}
