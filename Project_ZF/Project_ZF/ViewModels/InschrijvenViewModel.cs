namespace Project_ZF.ViewModels
{
    public class InschrijvenViewModel
    {
        public int GroepsreisId { get; set; }
        public string BestemmingsNaam { get; set; }
        public string GebruikerRol { get; set; }
        public int IsMonitor { get; set; }
        public List<KindViewModel> Kinderen { get; set; }
        public DateTime VertrekDatum { get; set; }
        public DateTime EindDatum { get; set; }
        public decimal Prijs { get; set; }

        public int StandaardPunten { get; set; }
        public MonitorViewModel Monitor { get; set; }
        public CustomUser User { get; set; } // Add this line
        public List<int> GeregistreerdeKinderenIds { get; set; } // Add this line
        public List<string> GeregistreerdeMonitorenIds { get; set; } = new List<string>();
    }
}