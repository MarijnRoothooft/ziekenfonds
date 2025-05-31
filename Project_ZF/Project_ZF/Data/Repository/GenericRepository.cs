using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Project_ZF.Data.Repository
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private readonly ZiekenFondsContext _context;

        // Constructor om de context te initialiseren
        public GenericRepository(ZiekenFondsContext context)
        {
            _context = context;
        }

        // Haal alle entiteiten asynchroon op
        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _context.Set<TEntity>().ToListAsync();
        }

        // Haal een entiteit op basis van ID asynchroon op
        public async Task<TEntity?> GetByIdAsync(int id)
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }

        // Methode om te zoeken naar groepsreizen op basis van een zoekwaarde
        public async Task<IEnumerable<TEntity>> GroepsreisSearch(Expression<Func<TEntity, bool>> zoekwaarde)
        {
            return await _context.Set<TEntity>().Where(zoekwaarde).ToListAsync();
        }

        // Voeg een nieuwe entiteit asynchroon toe
        public async Task AddAsync(TEntity entity)
        {
            try
            {
                await _context.Set<TEntity>().AddAsync(entity);
            }
            catch (Exception e)
            {
                throw new Exception("Error adding entity: " + e.Message);
            }
        }

        // Voeg een nieuwe entiteit synchroon toe
        public void Add(TEntity entity)
        {
            try
            {
                _context.Set<TEntity>().Add(entity);
            }
            catch (Exception e)
            {
                throw new Exception("Error adding entity: " + e.Message);
            }
        }

        // Zoek entiteiten op basis van voorwaarden en optionele includes
        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>>? voorwaarden, params Expression<Func<TEntity, object>>[]? includes)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();
            if (includes != null)
            {
                foreach (var item in includes)
                {
                    query = query.Include(item);
                }
            }
            if (voorwaarden != null)
            {
                query = query.Where(voorwaarden);
            }
            return query.ToList();
        }

        // Zoek entiteiten op basis van voorwaarden
        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> voorwaarden)
        {
            return Find(voorwaarden, null).ToList();
        }

        // Zoek entiteiten met optionele includes
        public IEnumerable<TEntity> Find(params Expression<Func<TEntity, object>>[] includes)
        {
            return Find(null, includes).ToList();
        }

        // Update een bestaande entiteit
        public void Update(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
        }

        // Verwijder een bestaande entiteit
        public void Delete(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
        }

        // Zoek entiteiten als IQueryable
        public IQueryable<TEntity> Search()
        {
            return _context.Set<TEntity>().AsQueryable();
        }

        // Sla de wijzigingen in de context op
        public void Save()
        {
            _context.SaveChanges();
        }

        // Nieuwe methode om alle entiteiten op te halen met optionele include properties
        public IEnumerable<TEntity> GetAll(Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "")
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();

            foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }

            return query.ToList();
        }

        // Haal alle entiteiten op inclusief externe lijsten (gebruikt om bestemmingen op te halen inclusief foto)
        public IEnumerable<TEntity> GetAllIncluding(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query.ToList();
        }

        // Haal entiteiten op met includes en een filter
        public async Task<IEnumerable<TEntity>> GetWithIncludesAsync(Expression<Func<TEntity, bool>> filter, params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return await query.ToListAsync();
        }

        public async Task<TEntity?> GetByIdIncludingAsync(int id, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return await query.FirstOrDefaultAsync(e => EF.Property<int>(e, "Id") == id);
        }
    }
}
