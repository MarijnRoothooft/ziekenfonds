using System.ComponentModel.DataAnnotations;

namespace Project_ZF.Models
{
    public class Opleiding
    {
        public int Id { get; set; }
        [Required]
        public string Naam { get; set; }
        [Required]
        public string Beschrijving { get; set; }
        [DataType(DataType.Date)]
        public DateTime BeginDatum { get; set; }
        [DataType(DataType.Date)]
        public DateTime EindDatum { get; set; }
        public int AantalPlaatsen { get; set; } 
        public int? OpleidingVereistId { get; set; }

        //verwijzing met OpleidingVereist naar Opleiding
        public Opleiding OpleidingVereist { get; set; } = default!;

        public List<Opleiding> VereisteOpleidingen { get; set; } = default!;
        public List<OpleidingPersoon> OpleidingPersonen { get; set; } = default!;


    }
}
