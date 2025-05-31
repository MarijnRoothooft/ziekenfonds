namespace Project_ZF.ViewModels
{
    public class MonitorViewModel
    {
        public MonitorViewModel()
        {
            Accounts = new List<CustomUser>();
            Groepsreizen = new List<Groepsreis>();
        }


        public int Id { get; set; }
        public string PersoonId { get; set; }
        public int GroepsreisDetailsId { get; set; }
        public int? IsHoofdMonitor { get; set; }

        public int Age
        {
            get
            {
                DateTime DOB = (DateTime)CustomUser.GeboorteDatum;
                DateTime now = DateTime.Today;
                int age = now.Year - DOB.Year;
                if (DOB > now.AddYears(-age)) age--;
                return age;
            }
        }


        public CustomUser CustomUser { get; set; } = default!;
        public Groepsreis Groepsreis { get; set; } = default!;


        public List<CustomUser> Accounts { get; set; }
        public List<Groepsreis> Groepsreizen { get; set; }
        public List<Models.Monitor> Monitors { get; set; }

    }
}

