using Project_ZF.Data;

namespace Project_ZF.Models
{
    public class LevelSeeding
    {
        private readonly ZiekenFondsContext _context;

        public LevelSeeding(ZiekenFondsContext context)
        {
            _context = context;
        }

        public void Seed()
        {
            SeedLevel();
        }

        public void SeedLevel()
        {
            if (!_context.Levels.Any())
            {
                IEnumerable<Level> levels = new List<Level>()
            {
                new Level()
                {
                    Id = 1,
                    BenodigdePunten=5,
                    Naam="Beginner"
                }

            };

                _context.Levels.AddRange(levels);
                _context.SaveChanges();
            }
        }
    }
}
