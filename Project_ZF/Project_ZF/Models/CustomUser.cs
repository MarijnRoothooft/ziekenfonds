using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Project_ZF.Models
{
    public class CustomUser : IdentityUser
    {

        [PersonalData]
        [Required]

        public string? Naam { get; set; }
        [PersonalData]
        [Required]
        public string? Voornaam { get; set; }
        [PersonalData]
        [Required]
        public string? Straat { get; set; }
        [PersonalData]
        [Required]
        public string? Huisnummer { get; set; }
        [PersonalData]
        [Required]
        public string? Gemeente { get; set; }
        [PersonalData]
        [Required]
        public string? Postcode { get; set; }
        [PersonalData]
        [Required]
        public DateTime? GeboorteDatum { get; set; }
        [PersonalData]
        [Required]
        public string? Huisdokter { get; set; }
        public bool IsLId { get; set; }
        [PersonalData]
        [Required]
        public override string? Email { get; set; }
        public bool IsHoofdMonitor { get; set; }
        [PersonalData]
        [Required]
        public string? TelefoonNummer { get; set; }
        [PersonalData]
        public string? RekeningNummer { get; set; }
        [Required]
        public bool IsActief { get; set; }
        public int AantalPunten {  get; set; }  
        public int LevelId { get ; set; }   


        public Level Level { get; set; }
        public List<Monitor> Monitoren { get; set; } = default!;
        public List<Kind> Kinderen { get; set; } = default!;
        public List<OpleidingPersoon> opleidingPersonen { get; set; } = default!;

    }
}
