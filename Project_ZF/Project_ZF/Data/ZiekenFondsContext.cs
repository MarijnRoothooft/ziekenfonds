using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Monitor = Project_ZF.Models.Monitor;

namespace Project_ZF.Data
{
    public class ZiekenFondsContext : IdentityDbContext<CustomUser>
    {
        public ZiekenFondsContext(DbContextOptions<ZiekenFondsContext> options)
            : base(options) { }
        public DbSet<Activiteit> Activiteiten { get; set; } = default!;
        public DbSet<Programma> Programmas { get; set; } = default!;
        public DbSet<Bestemming> Bestemmingen { get; set; } = default!;
        public DbSet<Foto> Fotos { get; set; } = default!;
        public DbSet<Onkosten> Onkosten { get; set; } = default!;
        public DbSet<Groepsreis> Groepsreizen { get; set; } = default!;
        public DbSet<Deelnemer> Deelnemers { get; set; } = default!;
        public DbSet<Monitor> Monitoren { get; set; } = default!;
        public DbSet<Kind> Kinderen { get; set; } = default!;
        public DbSet<Opleiding> Opleidingen { get; set; } = default!;
        public DbSet<OpleidingPersoon> OpleidingPersonen { get; set; } = default!;
        public DbSet<Punten> Punten { get; set; } = default!;
        public DbSet<Level> Levels { get; set; } = default !;
        public DbSet<Beloning> Beloningen { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Activiteit>().ToTable("Activiteit");
            modelBuilder.Entity<Programma>().ToTable("Programma");
            modelBuilder.Entity<Bestemming>().ToTable("Bestemming");
            modelBuilder.Entity<Foto>().ToTable("Foto");
            modelBuilder.Entity<Onkosten>().ToTable("Onkosten");
            modelBuilder.Entity<Groepsreis>().ToTable("Groepsreis");
            modelBuilder.Entity<Deelnemer>().ToTable("Deelnemer");
            modelBuilder.Entity<Monitor>().ToTable("Monitor");
            modelBuilder.Entity<Kind>().ToTable("Kind");
            modelBuilder.Entity<Opleiding>().ToTable("Opleiding");
            modelBuilder.Entity<OpleidingPersoon>().ToTable("OpleidingPersoon");
            modelBuilder.Entity<Punten>().ToTable("Punten");
            modelBuilder.Entity<Level>().ToTable("Level");
            modelBuilder.Entity<Beloning>().ToTable("Beloning");

            modelBuilder.Entity<Programma>()
                .HasOne(p => p.Activiteit)
                .WithMany(x => x.Programmas)
                .HasForeignKey(y => y.ActiviteitId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Programma>()
                .HasOne(p => p.Groepsreis)
                .WithMany(x => x.Programmas)
                .HasForeignKey(y => y.GroepsreisId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Bestemming>()
                .HasAlternateKey(b => b.Code);

            modelBuilder.Entity<Foto>()
                .HasOne(p => p.Bestemming)
                .WithMany(x => x.Fotos)
                .HasForeignKey(y => y.BestemmingId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Onkosten>()
                .HasOne(p => p.Groepsreis)
                .WithMany(x => x.Onkosten)
                .HasForeignKey(y => y.GroepsreisId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Groepsreis>()
                .HasOne(p => p.Bestemming)
                .WithMany(x => x.Groepsreizen)
                .HasForeignKey(y => y.GroepsreisId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Deelnemer>()
                .HasOne(p => p.Groepsreis)
                .WithMany(x => x.Deelnemers)
                .HasForeignKey(y => y.GroepreisDetailsId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Deelnemer>()
                .HasOne(p => p.Kind)
                .WithMany(x => x.Deelnemers)
                .HasForeignKey(y => y.KindId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Monitor>()
                .HasOne(p => p.Groepsreis)
                .WithMany(x => x.Monitoren)
                .HasForeignKey(y => y.GroepsreisDetailsId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Kind>()
                .HasOne(p => p.CustomUser)
                .WithMany(x => x.Kinderen)
                .HasForeignKey(y => y.PersoonId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Monitor>()
                .HasOne(p => p.CustomUser)
                .WithMany(x => x.Monitoren)
                .HasForeignKey(y => y.PersoonId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<OpleidingPersoon>()
                .HasOne(p => p.CustomUser)
                .WithMany(x => x.opleidingPersonen)
                .HasForeignKey(y => y.PersoonId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<Opleiding>()
                .HasOne(p => p.OpleidingVereist)
                .WithMany(x => x.VereisteOpleidingen)
                .HasForeignKey(y => y.OpleidingVereistId);


            modelBuilder.Entity<OpleidingPersoon>()
                .HasOne(p => p.Opleiding)
                .WithMany(x => x.OpleidingPersonen)
                .HasForeignKey(y => y.OpleidingsId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Punten>()
                .HasOne(p => p.Deelnemer)
                .WithMany(x => x.Punten) 
                .HasForeignKey(y => y.DeelnemerId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Punten>()
                .HasOne(p => p.Groepsreis)
                .WithMany(x => x.Punten)
                .HasForeignKey(y => y.GroepsreisId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<CustomUser>()
                .HasOne(p => p.Level)
                .WithMany(x => x.Gebruikers)
                .HasForeignKey(y => y.LevelId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Beloning>()
                .HasOne(p => p.Level)
                .WithMany(x => x.Beloningen)
                .HasForeignKey(y => y.LevelId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);




            ///////////////////////////////////////////////////
            /// DUMMY-DATA
            ///////////////////////////////////////////////////

            modelBuilder.Entity<Level>().HasData(
                new Level { Id = 1, Naam = "Beginner", BenodigdePunten = 0 },
                new Level { Id = 2, Naam = "Brons", BenodigdePunten = 500 },
                new Level { Id = 3, Naam = "Zilver", BenodigdePunten = 1000 },
                new Level { Id = 4, Naam = "Goud", BenodigdePunten = 6000 }

                );
            
             modelBuilder.Entity<Activiteit>().HasData(
             new Activiteit { Id = 1, Naam = "Zwemmen", Beschrijving = "Een verfrissende duik nemen in het zwembad." },
             new Activiteit { Id = 2, Naam = "Schilderen", Beschrijving = "Creatief aan de slag met verf en canvas." },
             new Activiteit { Id = 3, Naam = "Wandelen", Beschrijving = "Een ontspannen wandeling door de natuur." },
             new Activiteit { Id = 4, Naam = "Fietsen", Beschrijving = "Ontdek de omgeving met de fiets." },
             new Activiteit { Id = 5, Naam = "Koken", Beschrijving = "Leer nieuwe gerechten bereiden in de keuken." },
             new Activiteit { Id = 6, Naam = "Yoga", Beschrijving = "Kom tot rust en verbeter je flexibiliteit." },
             new Activiteit { Id = 7, Naam = "Boogschieten", Beschrijving = "Oefen je precisie met pijl en boog." },
             new Activiteit { Id = 8, Naam = "Kamperen", Beschrijving = "Geniet van een nacht in de buitenlucht." },
             new Activiteit { Id = 9, Naam = "Dansen", Beschrijving = "Beweeg op de ritmes van de muziek." },
             new Activiteit { Id = 10, Naam = "Fotografie", Beschrijving = "Leg prachtige momenten vast met je camera." }
          );
            modelBuilder.Entity<Beloning>().HasData(
                new Beloning { Id = 1, Beschrijving = "Niet accumuleerbaar met andere kortingen", LevelId = 1, Naam = "Spaar nog verder" },
                 new Beloning { Id = 2, Beschrijving = "Niet accumuleerbaar met andere kortingen", LevelId = 2, Naam = "100 euro korting" },
               new Beloning { Id = 3, Beschrijving = "Niet accumuleerbaar met andere kortingen", LevelId = 3, Naam = "200 euro korting" }

                ); ;


modelBuilder.Entity<Bestemming>().HasData(
            new Bestemming { Id = 1, Code = "AMS", BestemmingsNaam = "Amsterdam", Beschrijving = "Een mooie stad met grachten en musea.", MinLeeftijd = 16, MaxLeeftijd = 25 },
            new Bestemming { Id = 2, Code = "ROM", BestemmingsNaam = "Rome", Beschrijving = "De eeuwige stad met een rijke geschiedenis.", MinLeeftijd = 18, MaxLeeftijd = 28 },
            new Bestemming { Id = 3, Code = "PAR", BestemmingsNaam = "Parijs", Beschrijving = "De stad van de liefde en prachtige architectuur.", MinLeeftijd = 14, MaxLeeftijd = 22 },
            new Bestemming { Id = 4, Code = "BER", BestemmingsNaam = "Berlijn", Beschrijving = "Een stad met een boeiende geschiedenis en cultuur.", MinLeeftijd = 15, MaxLeeftijd = 24 },
            new Bestemming { Id = 5, Code = "LON", BestemmingsNaam = "Londen", Beschrijving = "Een kosmopolitische stad met iconische bezienswaardigheden.", MinLeeftijd = 17, MaxLeeftijd = 27 },
            new Bestemming { Id = 6, Code = "NYC", BestemmingsNaam = "New York", Beschrijving = "De stad die nooit slaapt.", MinLeeftijd = 20, MaxLeeftijd = 28 },
            new Bestemming { Id = 7, Code = "TOK", BestemmingsNaam = "Tokio", Beschrijving = "Een bruisende metropool met een mix van traditionele en moderne cultuur.", MinLeeftijd = 12, MaxLeeftijd = 21 },
            new Bestemming { Id = 8, Code = "SYD", BestemmingsNaam = "Sydney", Beschrijving = "Bekend om zijn iconische Opera House en prachtige stranden.", MinLeeftijd = 19, MaxLeeftijd = 26 },
            new Bestemming { Id = 9, Code = "CAI", BestemmingsNaam = "Caïro", Beschrijving = "De thuisbasis van de piramides en de Egyptische cultuur.", MinLeeftijd = 13, MaxLeeftijd = 23 },
            new Bestemming { Id = 10, Code = "CAP", BestemmingsNaam = "Kaapstad", Beschrijving = "Een stad met prachtige landschappen en diverse fauna.", MinLeeftijd = 18, MaxLeeftijd = 28 }
         );

         modelBuilder.Entity<CustomUser>().HasData(
             new CustomUser { Id = "1", Naam = "Jansen", Voornaam = "Jan", Straat = "Kerkstraat", Huisnummer = "12A", Gemeente = "Amsterdam", Postcode = "1234AB", GeboorteDatum = new DateTime(1990, 5, 20), Huisdokter = "Dr. Smith", IsLId = true, Email = "jan.jansen@example.com", IsHoofdMonitor = false, TelefoonNummer = "0612345678", RekeningNummer = "NL91ABNA0417164300", IsActief = true, UserName = "jan.jansen", NormalizedUserName = "JAN.JANSEN", NormalizedEmail = "JAN.JANSEN@EXAMPLE.COM", EmailConfirmed = true, PhoneNumber = "0612345678", PhoneNumberConfirmed = true, SecurityStamp = Guid.NewGuid().ToString(), LevelId=1 },
             new CustomUser { Id = "2", Naam = "De Vries", Voornaam = "Sophie", Straat = "Bakerstraat", Huisnummer = "34B", Gemeente = "Rotterdam", Postcode = "4321CD", GeboorteDatum = new DateTime(1985, 12, 15), Huisdokter = "Dr. Brown", IsLId = false, Email = "sophie.devries@example.com", IsHoofdMonitor = true, TelefoonNummer = "0612345679", RekeningNummer = "NL91ABNA0417164301", IsActief = true, UserName = "sophie.devries", NormalizedUserName = "SOPHIE.DEVRIES", NormalizedEmail = "SOPHIE.DEVRIES@EXAMPLE.COM", EmailConfirmed = true, PhoneNumber = "0612345679", PhoneNumberConfirmed = true, SecurityStamp = Guid.NewGuid().ToString(), LevelId=1 },
             new CustomUser { Id = "3", Naam = "Peters", Voornaam = "Kees", Straat = "Dorpstraat", Huisnummer = "5", Gemeente = "Utrecht", Postcode = "5678CD", GeboorteDatum = new DateTime(1992, 8, 10), Huisdokter = "Dr. Johnson", IsLId = true, Email = "kees.peters@example.com", IsHoofdMonitor = false, TelefoonNummer = "0612345680", RekeningNummer = "NL91ABNA0417164302", IsActief = true, UserName = "kees.peters", NormalizedUserName = "KEES.PETERS", NormalizedEmail = "KEES.PETERS@EXAMPLE.COM", EmailConfirmed = true, PhoneNumber = "0612345680", PhoneNumberConfirmed = true, SecurityStamp = Guid.NewGuid().ToString(), LevelId=1 },
             new CustomUser { Id = "4", Naam = "Bakker", Voornaam = "Els", Straat = "Hoofdstraat", Huisnummer = "9", Gemeente = "Den Haag", Postcode = "1234XY", GeboorteDatum = new DateTime(1980, 6, 22), Huisdokter = "Dr. White", IsLId = false, Email = "els.bakker@example.com", IsHoofdMonitor = true, TelefoonNummer = "0612345681", RekeningNummer = "NL91ABNA0417164303", IsActief = true, UserName = "els.bakker", NormalizedUserName = "ELS.BAKKER", NormalizedEmail = "ELS.BAKKER@EXAMPLE.COM", EmailConfirmed = true, PhoneNumber = "0612345681", PhoneNumberConfirmed = true, SecurityStamp = Guid.NewGuid().ToString(), LevelId=1 },
             new CustomUser { Id = "5", Naam = "Klein", Voornaam = "Tom", Straat = "Schoolstraat", Huisnummer = "44", Gemeente = "Eindhoven", Postcode = "1234EF", GeboorteDatum = new DateTime(1995, 3, 15), Huisdokter = "Dr. Green", IsLId = true, Email = "tom.klein@example.com", IsHoofdMonitor = false, TelefoonNummer = "0612345682", RekeningNummer = "NL91ABNA0417164304", IsActief = true, UserName = "tom.klein", NormalizedUserName = "TOM.KLEIN", NormalizedEmail = "TOM.KLEIN@EXAMPLE.COM", EmailConfirmed = true, PhoneNumber = "0612345682", PhoneNumberConfirmed = true, SecurityStamp = Guid.NewGuid().ToString(), LevelId=1 },
             new CustomUser { Id = "6", Naam = "Vermeer", Voornaam = "Lisa", Straat = "Zeeweg", Huisnummer = "1", Gemeente = "Groningen", Postcode = "8765AB", GeboorteDatum = new DateTime(1998, 11, 30), Huisdokter = "Dr. Black", IsLId = false, Email = "lisa.vermeer@example.com", IsHoofdMonitor = true, TelefoonNummer = "0612345683", RekeningNummer = "NL91ABNA0417164305", IsActief = true, UserName = "lisa.vermeer", NormalizedUserName = "LISA.VERMEER", NormalizedEmail = "LISA.VERMEER@EXAMPLE.COM", EmailConfirmed = true, PhoneNumber = "0612345683", PhoneNumberConfirmed = true, SecurityStamp = Guid.NewGuid().ToString(), LevelId=1 },
             new CustomUser { Id = "7", Naam = "Oosterman", Voornaam = "Peter", Straat = "Parklaan", Huisnummer = "20", Gemeente = "Tilburg", Postcode = "5012CD", GeboorteDatum = new DateTime(1991, 2, 25), Huisdokter = "Dr. Blue", IsLId = true, Email = "peter.oosterman@example.com", IsHoofdMonitor = false, TelefoonNummer = "0612345684", RekeningNummer = "NL91ABNA0417164306", IsActief = true, UserName = "peter.oosterman", NormalizedUserName = "PETER.OOSTERMAN", NormalizedEmail = "PETER.OOSTERMAN@EXAMPLE.COM", EmailConfirmed = true, PhoneNumber = "0612345684", PhoneNumberConfirmed = true, SecurityStamp = Guid.NewGuid().ToString(), LevelId=1 },
             new CustomUser { Id = "8", Naam = "Brouwer", Voornaam = "Anouk", Straat = "Vijverweg", Huisnummer = "15", Gemeente = "Nijmegen", Postcode = "6543AB", GeboorteDatum = new DateTime(1993, 9, 5), Huisdokter = "Dr. Grey", IsLId = false, Email = "anouk.brouwer@example.com", IsHoofdMonitor = true, TelefoonNummer = "0612345685", RekeningNummer = "NL91ABNA0417164307", IsActief = true, UserName = "anouk.brouwer", NormalizedUserName = "ANOUK.BROUWER", NormalizedEmail = "ANOUK.BROUWER@EXAMPLE.COM", EmailConfirmed = true, PhoneNumber = "0612345685", PhoneNumberConfirmed = true, SecurityStamp = Guid.NewGuid().ToString(), LevelId=1 },
             new CustomUser { Id = "9", Naam = "Kramer", Voornaam = "David", Straat = "Laan van de Vrijheid", Huisnummer = "30", Gemeente = "Haarlem", Postcode = "2030AB", GeboorteDatum = new DateTime(1988, 4, 10), Huisdokter = "Dr. Red", IsLId = true, Email = "david.kramer@example.com", IsHoofdMonitor = false, TelefoonNummer = "0612345686", RekeningNummer = "NL91ABNA0417164308", IsActief = true, UserName = "david.kramer", NormalizedUserName = "DAVID.KRAMER", NormalizedEmail = "DAVID.KRAMER@EXAMPLE.COM", EmailConfirmed = true, PhoneNumber = "0612345686", PhoneNumberConfirmed = true, SecurityStamp = Guid.NewGuid().ToString(), LevelId=1 },
             new CustomUser { Id = "10", Naam = "Smit", Voornaam = "Joris", Straat = "Weg naar de Vrijheid", Huisnummer = "11", Gemeente = "Breda", Postcode = "1234GH", GeboorteDatum = new DateTime(1989, 10, 5), Huisdokter = "Dr. Orange", IsLId = false, Email = "joris.smit@example.com", IsHoofdMonitor = true, TelefoonNummer = "0612345687", RekeningNummer = "NL91ABNA0417164309", IsActief = true, UserName = "joris.smit", NormalizedUserName = "JORIS.SMIT", NormalizedEmail = "JORIS.SMIT@EXAMPLE.COM", EmailConfirmed = true, PhoneNumber = "0612345687", PhoneNumberConfirmed = true, SecurityStamp = Guid.NewGuid().ToString(), LevelId=1 }
         );

         modelBuilder.Entity<Deelnemer>().HasData(
             new Deelnemer { Id = 1, KindId = 1, GroepreisDetailsId = 1, Opmerkingen = "Eerste deelnemer, enthousiast!", ReviewScore = 5, Review = "Geweldige ervaring!" },
             new Deelnemer { Id = 2, KindId = 2, GroepreisDetailsId = 2, Opmerkingen = "Zeer leergierig.", ReviewScore = 4, Review = "Zeer leerzaam." },
             new Deelnemer { Id = 3, KindId = 3, GroepreisDetailsId = 3, Opmerkingen = "Had veel plezier.", ReviewScore = 5, Review = "Wat een avontuur!" },
             new Deelnemer { Id = 4, KindId = 4, GroepreisDetailsId = 4, Opmerkingen = "Zou het zeker opnieuw doen.", ReviewScore = 5, Review = "Fantastisch!" },
             new Deelnemer { Id = 5, KindId = 5, GroepreisDetailsId = 5, Opmerkingen = "Een geweldige ervaring.", ReviewScore = 3, Review = "Leuke activiteit." },
             new Deelnemer { Id = 6, KindId = 6, GroepreisDetailsId = 6, Opmerkingen = "Ik heb er veel van geleerd.", ReviewScore = 4, Review = "Zeer leerzaam." },
             new Deelnemer { Id = 7, KindId = 7, GroepreisDetailsId = 7, Opmerkingen = "Mooi programma.", ReviewScore = 5, Review = "Wat een ervaring!" },
             new Deelnemer { Id = 8, KindId = 8, GroepreisDetailsId = 8, Opmerkingen = "Zeer goed georganiseerd.", ReviewScore = 4, Review = "Alles was perfect geregeld." },
             new Deelnemer { Id = 9, KindId = 9, GroepreisDetailsId = 9, Opmerkingen = "Prachtige ervaring.", ReviewScore = 5, Review = "Een onvergetelijke reis!" },
             new Deelnemer { Id = 10, KindId = 10, GroepreisDetailsId = 10, Opmerkingen = "Echt een aanrader.", ReviewScore = 4, Review = "Zeker de moeite waard!" }
         );

         //modelBuilder.Entity<ErrorViewModel>().HasData(
         //    new ErrorViewModel { RequestId = "REQ-001" },
         //    new ErrorViewModel { RequestId = "REQ-002" },
         //    new ErrorViewModel { RequestId = "REQ-003" },
         //    new ErrorViewModel { RequestId = "REQ-004" },
         //    new ErrorViewModel { RequestId = "REQ-005" },
         //    new ErrorViewModel { RequestId = "REQ-006" },
         //    new ErrorViewModel { RequestId = "REQ-007" },
         //    new ErrorViewModel { RequestId = "REQ-007" },
         //    new ErrorViewModel { RequestId = "REQ-008" },
         //    new ErrorViewModel { RequestId = "REQ-009" },
         //    new ErrorViewModel { RequestId = "REQ-010" }
         //);

         modelBuilder.Entity<Foto>().HasData(
             new Foto { Id = 1, Naam = "AmsterdamGrachten.jpg", BestemmingId = 1 },
             new Foto { Id = 2, Naam = "RomeColosseum.jpg", BestemmingId = 2 },
             new Foto { Id = 3, Naam = "ParijsEiffeltoren.jpg", BestemmingId = 3 },
             new Foto { Id = 4, Naam = "BerlijnBrandenburgerTor.jpg", BestemmingId = 4 },
             new Foto { Id = 5, Naam = "LondenBigBen.jpg", BestemmingId = 5 },
             new Foto { Id = 6, Naam = "NewYorkSkyline.jpg", BestemmingId = 6 },
             new Foto { Id = 7, Naam = "TokioShibuya.jpg", BestemmingId = 7 },
             new Foto { Id = 8, Naam = "SydneyOperaHouse.jpg", BestemmingId = 8 },
             new Foto { Id = 9, Naam = "CairoPyramids.jpg", BestemmingId = 9 },
             new Foto { Id = 10, Naam = "KaapstadTafelberg.jpg", BestemmingId = 10 }
         );

         modelBuilder.Entity<Groepsreis>().HasData(
             new Groepsreis { Id = 1, GroepsreisId = 1, BeginDatum = new DateTime(2024, 06, 01), EindDatum = new DateTime(2024, 06, 15), Prijs = 1200.00, StandaardPunten=200 },
             new Groepsreis { Id = 2, GroepsreisId = 2, BeginDatum = new DateTime(2025, 07, 10), EindDatum = new DateTime(2025, 07, 24), Prijs = 1500.00 , StandaardPunten=300},
             new Groepsreis { Id = 3, GroepsreisId = 3, BeginDatum = new DateTime(2025, 08, 15), EindDatum = new DateTime(2025, 08, 30), Prijs = 1400.00, StandaardPunten=100 },
             new Groepsreis { Id = 4, GroepsreisId = 4, BeginDatum = new DateTime(2024, 09, 01), EindDatum = new DateTime(2024, 09, 15), Prijs = 1600.00,  StandaardPunten =200},
             new Groepsreis { Id = 5, GroepsreisId = 5, BeginDatum = new DateTime(2024, 10, 10), EindDatum = new DateTime(2024, 10, 24), Prijs = 1300.00, StandaardPunten=100 },
             new Groepsreis { Id = 6, GroepsreisId = 6, BeginDatum = new DateTime(2024, 11, 01), EindDatum = new DateTime(2024, 11, 15), Prijs = 1700.00, StandaardPunten=200 },
             new Groepsreis { Id = 7, GroepsreisId = 7, BeginDatum = new DateTime(2025, 12, 01), EindDatum = new DateTime(2025, 12, 15), Prijs = 1800.00 , StandaardPunten=300},
             new Groepsreis { Id = 8, GroepsreisId = 8, BeginDatum = new DateTime(2025, 01, 05), EindDatum = new DateTime(2025, 01, 20), Prijs = 1900.00, StandaardPunten=200 },
             new Groepsreis { Id = 9, GroepsreisId = 9, BeginDatum = new DateTime(2025, 02, 15), EindDatum = new DateTime(2025, 03, 01), Prijs = 1600.00, StandaardPunten=100 },
             new Groepsreis { Id = 10, GroepsreisId = 10, BeginDatum = new DateTime(2025, 03, 10), EindDatum = new DateTime(2025, 03, 25), Prijs = 2000.00, StandaardPunten=50 }
         );


         modelBuilder.Entity<Kind>().HasData(
         new Kind { Id = 1, PersoonId = "1", Naam = "Jansen", Voornaam = "Anna", Geboortedatum = new DateTime(2012, 04, 15), Allergieën = "Pinda's", Medicatie = "Geen" },
         new Kind { Id = 2, PersoonId = "2", Naam = "De Vries", Voornaam = "Luca", Geboortedatum = new DateTime(2011, 08, 22), Allergieën = "Geen", Medicatie = "Astma-inhaler" },
         new Kind { Id = 3, PersoonId = "3", Naam = "Peters", Voornaam = "Sophie", Geboortedatum = new DateTime(2013, 03, 10), Allergieën = "Koemelk", Medicatie = "Geen" },
         new Kind { Id = 4, PersoonId = "4", Naam = "Meijer", Voornaam = "Noah", Geboortedatum = new DateTime(2010, 11, 05), Allergieën = "Gluten", Medicatie = "Geen" },
         new Kind { Id = 5, PersoonId = "5", Naam = "Van Dijk", Voornaam = "Emma", Geboortedatum = new DateTime(2014, 07, 30), Allergieën = "Eieren", Medicatie = "Geen" },
         new Kind { Id = 6, PersoonId = "6", Naam = "Kok", Voornaam = "Liam", Geboortedatum = new DateTime(2011, 09, 18), Allergieën = "Geen", Medicatie = "Geen" },
         new Kind { Id = 7, PersoonId = "7", Naam = "Smit", Voornaam = "Mila", Geboortedatum = new DateTime(2013, 01, 25), Allergieën = "Noten", Medicatie = "Geen" },
         new Kind { Id = 8, PersoonId = "8", Naam = "Hendriks", Voornaam = "Tijn", Geboortedatum = new DateTime(2012, 02, 14), Allergieën = "Geen", Medicatie = "Antibiotica" },
         new Kind { Id = 9, PersoonId = "9", Naam = "Kramer", Voornaam = "Sanne", Geboortedatum = new DateTime(2010, 12, 21), Allergieën = "Fruit", Medicatie = "Geen" },
         new Kind { Id = 10, PersoonId = "10", Naam = "Dekker", Voornaam = "Julian", Geboortedatum = new DateTime(2015, 06, 05), Allergieën = "Geen", Medicatie = "Geen" }
     );


         modelBuilder.Entity<Monitor>().HasData(
             new Monitor { Id = 1, PersoonId = "1", GroepsreisDetailsId = 1, IsHoofdMonitor = 1 },
             new Monitor { Id = 2, PersoonId = "2", GroepsreisDetailsId = 2, IsHoofdMonitor = null },
             new Monitor { Id = 3, PersoonId = "3", GroepsreisDetailsId = 3, IsHoofdMonitor = 1 },
             new Monitor { Id = 4, PersoonId = "4", GroepsreisDetailsId = 4, IsHoofdMonitor = null },
             new Monitor { Id = 5, PersoonId = "5", GroepsreisDetailsId = 5, IsHoofdMonitor = 1 },
             new Monitor { Id = 6, PersoonId = "6", GroepsreisDetailsId = 6, IsHoofdMonitor = null },
             new Monitor { Id = 7, PersoonId = "7", GroepsreisDetailsId = 7, IsHoofdMonitor = 1 },
             new Monitor { Id = 8, PersoonId = "8", GroepsreisDetailsId = 8, IsHoofdMonitor = null },
             new Monitor { Id = 9, PersoonId = "9", GroepsreisDetailsId = 9, IsHoofdMonitor = 1 },
             new Monitor { Id = 10, PersoonId = "10", GroepsreisDetailsId = 10, IsHoofdMonitor = null }
         );

         modelBuilder.Entity<Onkosten>().HasData(
             new Onkosten { Id = 1, GroepsreisId = 1, Titel = "Vervoer", Omschrijving = "Kosten voor busvervoer naar Amsterdam.", Bedrag = 200.00, Datum = new DateTime(2024, 06, 01), Foto = "VervoerAmsterdam.jpg" },
             new Onkosten { Id = 2, GroepsreisId = 2, Titel = "Accommodatie", Omschrijving = "Hotelovernachtingen in Amsterdam.", Bedrag = 800.00, Datum = new DateTime(2024, 06, 02), Foto = "HotelAmsterdam.jpg" },
             new Onkosten { Id = 3, GroepsreisId = 3, Titel = "Vervoer", Omschrijving = "Vliegtickets naar Rome.", Bedrag = 300.00, Datum = new DateTime(2024, 07, 10), Foto = "VliegticketsRome.jpg" },
             new Onkosten { Id = 4, GroepsreisId = 4, Titel = "Excursies", Omschrijving = "Toegang tot het Colosseum.", Bedrag = 50.00, Datum = new DateTime(2024, 07, 12), Foto = "ColosseumExcursie.jpg" },
             new Onkosten { Id = 5, GroepsreisId = 5, Titel = "Vervoer", Omschrijving = "Treinreis naar Parijs.", Bedrag = 150.00, Datum = new DateTime(2024, 08, 15), Foto = "TreinreisParijs.jpg" },
             new Onkosten { Id = 6, GroepsreisId = 6, Titel = "Eten", Omschrijving = "Maaltijden tijdens het verblijf.", Bedrag = 200.00, Datum = new DateTime(2024, 08, 16), Foto = "EtenParijs.jpg" },
             new Onkosten { Id = 7, GroepsreisId = 7, Titel = "Activiteiten", Omschrijving = "Stadstour door Berlijn.", Bedrag = 100.00, Datum = new DateTime(2024, 09, 01), Foto = "BerlijnStadstour.jpg" },
             new Onkosten { Id = 8, GroepsreisId = 8, Titel = "Vervoer", Omschrijving = "Metrokaartjes in Londen.", Bedrag = 50.00, Datum = new DateTime(2024, 10, 10), Foto = "MetroLondon.jpg" },
             new Onkosten { Id = 9, GroepsreisId = 9, Titel = "Excursies", Omschrijving = "Toegang tot het Empire State Building.", Bedrag = 40.00, Datum = new DateTime(2024, 11, 01), Foto = "EmpireStateBuilding.jpg" },
             new Onkosten { Id = 10, GroepsreisId = 10, Titel = "Vervoer", Omschrijving = "Shinkansen-tickets naar Tokio.", Bedrag = 250.00, Datum = new DateTime(2024, 12, 10), Foto = "Shinkansen.jpg" }
         );

         modelBuilder.Entity<Opleiding>().HasData(
             new Opleiding { Id = 1, Naam = "Basisopleiding Reisleider", Beschrijving = "Een introductiecursus voor beginnende reisleiders.", BeginDatum = new DateTime(2024, 05, 01), EindDatum = new DateTime(2024, 05, 30), AantalPlaatsen = 20, OpleidingVereistId = null },
             new Opleiding { Id = 2, Naam = "Geavanceerde Reisleiding", Beschrijving = "Verdiepingscursus voor ervaren reisleiders.", BeginDatum = new DateTime(2024, 06, 10), EindDatum = new DateTime(2024, 07, 10), AantalPlaatsen = 15, OpleidingVereistId = 1 },
             new Opleiding { Id = 3, Naam = "Cultuur en Geschiedenis", Beschrijving = "Cursus over de culturele en historische aspecten van verschillende bestemmingen.", BeginDatum = new DateTime(2024, 08, 01), EindDatum = new DateTime(2024, 08, 31), AantalPlaatsen = 30, OpleidingVereistId = null },
             new Opleiding { Id = 4, Naam = "Taalvaardigheid voor Reisleiders", Beschrijving = "Cursus gericht op taalvaardigheden voor communicatie met klanten.", BeginDatum = new DateTime(2024, 09, 01), EindDatum = new DateTime(2024, 09, 30), AantalPlaatsen = 25, OpleidingVereistId = 2 },
             new Opleiding { Id = 5, Naam = "Ethische Reispraktijken", Beschrijving = "Cursus over duurzaamheid en ethische overwegingen in de reisindustrie.", BeginDatum = new DateTime(2024, 10, 05), EindDatum = new DateTime(2024, 10, 20), AantalPlaatsen = 20, OpleidingVereistId = null },
             new Opleiding { Id = 6, Naam = "Veiligheid en Noodprocedures", Beschrijving = "Training over veiligheidsprocedures en hoe te handelen in noodgevallen.", BeginDatum = new DateTime(2024, 11, 01), EindDatum = new DateTime(2024, 11, 15), AantalPlaatsen = 20, OpleidingVereistId = 1 },
             new Opleiding { Id = 7, Naam = "Klantenservice in de Reisbranche", Beschrijving = "Cursus die zich richt op het verbeteren van klantenservicevaardigheden.", BeginDatum = new DateTime(2024, 12, 01), EindDatum = new DateTime(2024, 12, 20), AantalPlaatsen = 15, OpleidingVereistId = null },
             new Opleiding { Id = 8, Naam = "Technieken voor Groepsmanagement", Beschrijving = "Training voor het effectief beheren van groepen tijdens reizen.", BeginDatum = new DateTime(2025, 01, 10), EindDatum = new DateTime(2025, 01, 25), AantalPlaatsen = 15, OpleidingVereistId = 2 },
             new Opleiding { Id = 9, Naam = "Reisplanning en Logistiek", Beschrijving = "Cursus over het plannen en organiseren van reizen.", BeginDatum = new DateTime(2025, 02, 05), EindDatum = new DateTime(2025, 02, 20), AantalPlaatsen = 25, OpleidingVereistId = null },
             new Opleiding { Id = 10, Naam = "Marketing voor Reisleiders", Beschrijving = "Training over marketingstrategieën specifiek voor de reisbranche.", BeginDatum = new DateTime(2025, 03, 01), EindDatum = new DateTime(2025, 03, 15), AantalPlaatsen = 20, OpleidingVereistId = null }
         );

         modelBuilder.Entity<OpleidingPersoon>().HasData(
             new OpleidingPersoon { Id = 1, OpleidingsId = 1, PersoonId = "1" },
             new OpleidingPersoon { Id = 2, OpleidingsId = 2, PersoonId = "2" },
             new OpleidingPersoon { Id = 3, OpleidingsId = 3, PersoonId = "3" },
             new OpleidingPersoon { Id = 4, OpleidingsId = 4, PersoonId = "4" },
             new OpleidingPersoon { Id = 5, OpleidingsId = 5, PersoonId = "5" },
             new OpleidingPersoon { Id = 6, OpleidingsId = 6, PersoonId = "6" },
             new OpleidingPersoon { Id = 7, OpleidingsId = 7, PersoonId = "7" },
             new OpleidingPersoon { Id = 8, OpleidingsId = 8, PersoonId = "8" },
             new OpleidingPersoon { Id = 9, OpleidingsId = 9, PersoonId = "9" },
             new OpleidingPersoon { Id = 10, OpleidingsId = 10, PersoonId = "10" }
         );
            modelBuilder.Entity<Punten>().HasData(
                new Punten { Id=1, Omschrijving="deelname reis", AantalPunten=200, Datum = new DateTime(2025, 12, 01) , DeelnemerId=1, GroepsreisId=1},
                new Punten { Id = 2, Omschrijving = "deelname reis", AantalPunten = 100, Datum = new DateTime(2025, 12, 01), DeelnemerId = 2, GroepsreisId = 2 }
            );

         modelBuilder.Entity<Programma>().HasData(
             new Programma { Id = 1, ActiviteitId = 1, GroepsreisId = 1 },
             new Programma { Id = 2, ActiviteitId = 2, GroepsreisId = 2 },
             new Programma { Id = 3, ActiviteitId = 3, GroepsreisId = 3 },
             new Programma { Id = 4, ActiviteitId = 4, GroepsreisId = 4 },
             new Programma { Id = 5, ActiviteitId = 5, GroepsreisId = 5 },
             new Programma { Id = 6, ActiviteitId = 6, GroepsreisId = 6 },
             new Programma { Id = 7, ActiviteitId = 7, GroepsreisId = 7 },
             new Programma { Id = 8, ActiviteitId = 8, GroepsreisId = 8 },
             new Programma { Id = 9, ActiviteitId = 9, GroepsreisId = 9 },
             new Programma { Id = 10, ActiviteitId = 10, GroepsreisId = 10 }
         );
        }
    }
}
