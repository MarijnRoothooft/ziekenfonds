using Project_ZF.Data;

namespace Project_ZF.Models
{
    public class SeedingDummyData
    {
        private readonly ZiekenFondsContext _context;

        public SeedingDummyData(ZiekenFondsContext context)
        {
            _context = context;
        }

        public void Seed()
        {
            SeedGroepsreis();
        }

        public void SeedGroepsreis()
        {
            if (!_context.Groepsreizen.Any())
            {
                IEnumerable<Groepsreis> groepsreizen = new List<Groepsreis>()
            {
                new Groepsreis()
                {
                    Id = 1,
                    BeginDatum= new DateTime(), 
                    EindDatum= DateTime.Parse("2016-05-02"),


            }

            };

                _context.Groepsreizen.AddRange(groepsreizen);
                _context.SaveChanges();
            }
        }
    }
}

