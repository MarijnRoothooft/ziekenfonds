namespace Project_ZF.ViewModels
{
    /// ViewModel voor het weergeven van een bestemming.
    public class BestemmingViewModel
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string BestemmingsNaam { get; set; }
        public string Beschrijving { get; set; }
        public int MinLeeftijd { get; set; }
        public int MaxLeeftijd { get; set; }
        // Geeft de leeftijdsrange weer als een string
        public string LeeftijdRange => $"{MinLeeftijd} - {MaxLeeftijd}";
        // Lijst van foto's die bij de bestemming horen.
        public List<FotoViewModel> Fotos { get; set; } = new List<FotoViewModel>();
        // Lijst van groepsreizen die bij de bestemming horen.
        public List<GroepsreisViewModel> Groepsreizen { get; set; } = new List<GroepsreisViewModel>();
    }


}
