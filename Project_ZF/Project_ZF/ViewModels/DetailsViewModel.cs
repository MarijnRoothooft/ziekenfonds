using System.ComponentModel.DataAnnotations;

namespace Project_ZF.ViewModels
{
    public class DetailsViewModel
    {
        public int Id { get; set; }
        public string Naam { get; set; }
        public string BestemmingsNaam { get; set; }
        public string Beschrijving { get; set; }
        public int MinLeeftijd { get; set; }
        public int MaxLeeftijd { get; set; }
        public List<Foto> Fotos { get; set; }

        // Nieuwe eigenschappen
        public List<ActiviteitViewModel> Activiteiten { get; set; } = new List<ActiviteitViewModel>();
        public DateTime VertrekDatum { get; set; }
        public DateTime EindDatum { get; set; }
        public double Prijs { get; set; }
        public int StandaardPunten { get; set; }    

        // Review eigenschappen
        public int DeelnemerId { get; set; }
        public int KindId { get; set; }
        public int GroepsreisId { get; set; }
        public List<ReviewViewModel> EerdereReviews { get; set; } = new List<ReviewViewModel>();
        public double AverageScore { get; set; }
        public bool HeeftKind { get; set; }
    }

    public class ReviewViewModel
    {
        public int ReviewScore { get; set; }
        public string Review { get; set; }
        public string Opmerkingen { get; set; }
        public string KindNaam { get; set; }
        public int DeelnemerId { get; set; }
        public int KindId { get; set; }
    }
    public class ReviewCreateViewModel
    {
        public int DeelnemerId { get; set; }
        public int KindId { get; set; }
        public int GroepsreisId { get; set; }
        public int FotoId { get; set; }

        [Required]
        [Range(1, 5, ErrorMessage = "De review score moet tussen 1 en 5 liggen.")]
        public int ReviewScore { get; set; }

        [Required]
        [StringLength(1000, ErrorMessage = "De review mag niet langer zijn dan 1000 tekens.")]
        public string Review { get; set; }

        [StringLength(1000, ErrorMessage = "De opmerkingen mogen niet langer zijn dan 1000 tekens.")]
        public string Opmerkingen { get; set; }
    }

}