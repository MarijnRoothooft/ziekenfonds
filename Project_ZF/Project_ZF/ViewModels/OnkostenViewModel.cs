namespace Project_ZF.ViewModels
{
    public class OnkostenViewModel
    {
        public int Id { get; set; }
        public string BestemmingsNaam { get; set; } // Naam van de bestemming
        public string Titel { get; set; }
        public string Omschrijving { get; set; }
        public double Bedrag { get; set; }
        public DateTime Datum { get; set; }
        public string? Foto { get; set; }
    }
}

