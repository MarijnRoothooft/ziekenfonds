namespace Project_ZF.ViewModels
{
    public class MonitorBeheerDetailViewModel
    {
        public string Naam { get; set; }
        public string Voornaam { get; set; }

        public IEnumerable<Models.Monitor> MonitorReizen { get; set; }
        public IEnumerable<OpleidingPersoon> Opleidingen { get; set; }
    }
}
