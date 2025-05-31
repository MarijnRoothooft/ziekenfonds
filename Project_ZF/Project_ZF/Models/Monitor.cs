namespace Project_ZF.Models
{
    public class Monitor
    {
        public int Id { get; set; }
        public string PersoonId { get; set; }
        public int GroepsreisDetailsId { get; set; }
        public int? IsHoofdMonitor { get; set; }

        public CustomUser CustomUser { get; set; } = default!;
        public Groepsreis Groepsreis { get; set; } = default!;
    }
}