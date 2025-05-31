using System.ComponentModel.DataAnnotations;

namespace Project_ZF.Models
{
    public class Deelnemer
    {
        public int Id { get; set; }
        public int KindId { get; set; }
        public int GroepreisDetailsId { get; set; }
        public string Opmerkingen { get; set; }
        public int? ReviewScore { get; set; }
        public string Review {  get; set; } 
        public Groepsreis Groepsreis { get; set; } = default!;
        public Kind Kind { get; set; } = default!;
        public List<Punten> Punten {  get; set; } = new List<Punten>();
    }
}
