namespace Project_ZF.Data.Repository.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ZiekenFondsContext _ziekenfondsContext;

        public UnitOfWork(ZiekenFondsContext context)
        {
            _ziekenfondsContext = context;
            CustomUserRepository = new GenericRepository<CustomUser>(_ziekenfondsContext);
            FotoRepository = new GenericRepository<Foto>(_ziekenfondsContext);
            BestemmingRepository = new GenericRepository<Bestemming>(_ziekenfondsContext);
            OpleidingRepository = new GenericRepository<Opleiding>(_ziekenfondsContext);
            KindRepository = new GenericRepository<Kind>(_ziekenfondsContext);
            ProgrammaRepository = new GenericRepository<Programma>(_ziekenfondsContext);
            ActiviteitRepository = new GenericRepository<Activiteit>(_ziekenfondsContext);
            GroepsreisRepository = new GenericRepository<Groepsreis>(_ziekenfondsContext);
            DeelnemerRepository = new GenericRepository<Deelnemer>(_ziekenfondsContext);
            MonitorRepository = new GenericRepository<Models.Monitor>(_ziekenfondsContext);
            OpleidingPersoonRepository = new GenericRepository<OpleidingPersoon>(_ziekenfondsContext);
            OnkostenRepository = new GenericRepository<Onkosten>(_ziekenfondsContext);
            PuntenRepository = new GenericRepository<Punten>(_ziekenfondsContext);
            LevelRepository = new GenericRepository<Level>(_ziekenfondsContext);
            BeloningRepository = new GenericRepository<Beloning>(_ziekenfondsContext);    


        }

        public IGenericRepository<CustomUser> CustomUserRepository { get; }
        public IGenericRepository<Foto> FotoRepository { get; }
        public IGenericRepository<Bestemming> BestemmingRepository { get; }
        public IGenericRepository<Opleiding> OpleidingRepository { get; }
        public IGenericRepository<Kind> KindRepository { get; }
        public IGenericRepository<Programma> ProgrammaRepository { get; }
        public IGenericRepository<Activiteit> ActiviteitRepository { get; }
        public IGenericRepository<Groepsreis> GroepsreisRepository { get; }
        public IGenericRepository<Deelnemer> DeelnemerRepository { get; }
        public IGenericRepository<Models.Monitor> MonitorRepository { get; }
        public IGenericRepository<OpleidingPersoon> OpleidingPersoonRepository { get; }
        public IGenericRepository<Onkosten> OnkostenRepository { get; }
        public IGenericRepository<Punten> PuntenRepository { get; }
        public IGenericRepository<Level> LevelRepository { get; }
        public IGenericRepository<Beloning> BeloningRepository { get; }



        public void SaveChanges()
        {
            _ziekenfondsContext.SaveChanges();
        }
        public async Task SaveChangesAsync()
        {
            await _ziekenfondsContext.SaveChangesAsync();
        }
    }
}