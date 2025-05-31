namespace Project_ZF.ViewModels
{
    public class DashboardViewModel
    {
        public CustomUser User { get; set; }

        public IEnumerable<Groepsreis> Groepsreizen { get; set; }   
        public IEnumerable<Deelnemer> Reizen { get; set; }
        public IEnumerable<Models.Monitor> MonitorReizen { get; set; }
        public IEnumerable<OpleidingPersoon> Opleidingen { get; set; }
        public IEnumerable<Kind> Kinderen { get; set; }

        public IEnumerable <Groepsreis> BeheerdersGroepreizen { get; set; }
    }
}
