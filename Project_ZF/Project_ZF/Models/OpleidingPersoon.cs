namespace Project_ZF.Models
{
    public class OpleidingPersoon
    {
        public int Id { get; set; }
        public int OpleidingsId { get; set; }
        public string PersoonId {  get; set; }

        public Opleiding Opleiding { get; set; } = default!;
        public CustomUser CustomUser { get; set; } = default!;

    }
}
