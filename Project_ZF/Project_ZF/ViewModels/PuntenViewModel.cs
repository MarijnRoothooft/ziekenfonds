using System.ComponentModel.DataAnnotations;

namespace Project_ZF.ViewModels
{
    public class PuntenViewModel
    {
        public int AantalPunten { get; set; }
        [DataType(DataType.Date)]
        public DateTime Datum { get; set; }
        [Required]
        public string Omschrijving { get; set; }
        public IEnumerable<Punten> Groepsreizen { get; set; }
        
        public string Voornaam { get; set; }
        public string Achternaam { get; set; }

        public IEnumerable<CustomUser> LevelBeloning { get; set; }
    }
}
