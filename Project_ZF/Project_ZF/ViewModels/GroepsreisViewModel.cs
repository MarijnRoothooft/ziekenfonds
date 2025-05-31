using System.ComponentModel.DataAnnotations;

namespace Project_ZF.ViewModels
{
    public class GroepsreisViewModel
    {
        public int Id { get; set; }
        public int GroepsreisId { get; set; }

        [DataType(DataType.Date)]
        public DateTime BeginDatum { get; set; }

        [DataType(DataType.Date)]
        public DateTime EindDatum { get; set; }
        public double Prijs { get; set; }
        public int StandaardPunten { get; set; }
        public string BestemmingsNaam { get; set; }
        public List<Onkosten> Onkosten { get; set; } = new List<Onkosten>();
        public List<string> Deelnemers { get; set; } = new List<string>();
        public List<string> Monitoren { get; set; } = new List<string>();
        public List<ProgrammaViewModel> Programmas { get; set; } = new List<ProgrammaViewModel>();
    }
}