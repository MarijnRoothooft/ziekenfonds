namespace Project_ZF.Data.Repository.UnitOfWork
{
    public interface IUnitOfWork
    {
        IGenericRepository<CustomUser> CustomUserRepository { get; }
        IGenericRepository<Foto> FotoRepository { get; }
        IGenericRepository<Bestemming> BestemmingRepository { get; }
        IGenericRepository<Opleiding> OpleidingRepository { get; }
        IGenericRepository<Kind> KindRepository { get; }
        IGenericRepository<Programma> ProgrammaRepository { get; }
        IGenericRepository<Activiteit> ActiviteitRepository { get; }
        IGenericRepository<Groepsreis> GroepsreisRepository { get; }
        IGenericRepository<Deelnemer> DeelnemerRepository { get; }
        IGenericRepository<Models.Monitor> MonitorRepository { get; }
        IGenericRepository<OpleidingPersoon> OpleidingPersoonRepository { get; }
        IGenericRepository<Onkosten> OnkostenRepository { get; }
        IGenericRepository<Punten> PuntenRepository { get; }
        IGenericRepository<Level> LevelRepository { get; }
        IGenericRepository<Beloning> BeloningRepository { get; }


        public void SaveChanges();
        Task SaveChangesAsync();
    }
}