using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Project_ZF.Controllers;
using Project_ZF.Data;
using System.Data.Common;
using System.Reflection.Metadata;

namespace Project_ZF.Models
{
    public class IdentitySeeding
    {
        private readonly ZiekenFondsContext _context;

        public IdentitySeeding(ZiekenFondsContext context) {
            _context = context;
                }


        public async Task IdentitySeedingAsync(UserManager<CustomUser> userManager, RoleManager<IdentityRole> roleManager)
        {  
          
            { 

                try
                {
                   bool role = await roleManager.RoleExistsAsync("gebruiker");
                   if (!role) await roleManager.CreateAsync(new IdentityRole("gebruiker"));

                    role = await roleManager.RoleExistsAsync("monitor");
                    if (!role) await roleManager.CreateAsync(new IdentityRole("monitor"));


                    role = await roleManager.RoleExistsAsync("verantwoordelijke");
                    if (!role) await roleManager.CreateAsync(new IdentityRole("verantwoordelijke"));


                    role = await roleManager.RoleExistsAsync("beheerder");
                 if (!role) await roleManager.CreateAsync(new IdentityRole("beheerder"));


                    if (userManager.FindByNameAsync("Admin").Result == null)
                    {
                        var defaultUser = new CustomUser
                        {
                            Naam = "Admin",
                            UserName="Admin",
                            Voornaam = "Admin",
                            GeboorteDatum = new DateTime(25/12/2002),
                            Straat = "Antwerpsestraat",
                            Postcode = "3232",
                            Gemeente = "Lier",
                            TelefoonNummer = "014 56 44 50",
                            Huisnummer = "99",
                            Huisdokter = "Dr. Thomas",
                            Email = "admin@gmail.com",
                            IsActief=true,
                            AantalPunten = 0,
                            LevelId= 1 
                        };


                        // Gebruiker aanmaken met rol
                        var admin = await userManager.CreateAsync(defaultUser, "t0LTHxzy.v");
                      if (admin.Succeeded && userManager.FindByNameAsync("Admin").Result != null)
                      
                            
                            
                            await userManager.AddToRoleAsync(defaultUser, "beheerder");
                    }

                    if (userManager.FindByNameAsync("User").Result == null)
                    {
                        var defaultUser1 = new CustomUser
                        {
                            Naam = "User",
                            UserName = "User",
                            Voornaam = "User",
                            GeboorteDatum = new DateTime(25 / 12 / 2002),
                            Straat = "Antwerpsestraat",
                            Postcode = "3232",
                            Gemeente = "Lier",
                            TelefoonNummer = "014 56 44 50",
                            Huisnummer = "99",
                            Huisdokter = "Dr. Thomas",
                            Email = "user@gmail.com",
                            IsActief = true,
                            AantalPunten = 0,
                            LevelId = 1
                        };
                        var admin = await userManager.CreateAsync(defaultUser1, "User-1");

                        if (admin.Succeeded && userManager.FindByNameAsync("UserenKind").Result != null)
                            await userManager.AddToRoleAsync(defaultUser1, "gebruiker");
                    }

                        if (userManager.FindByNameAsync("UserenKind").Result == null)
                    {
                        var defaultUser = new CustomUser
                        {
                            Naam = "UserenKind",
                            UserName = "UserenKind",
                            Voornaam = "Userenkind",
                            GeboorteDatum = new DateTime(25/12/2002),
                            Straat = "Antwerpsestraat",
                            Postcode = "3232",
                            Gemeente = "Lier",
                            TelefoonNummer = "014 56 44 50",
                            Huisnummer = "99",
                            Huisdokter = "Dr. Thomas",
                            Email = "userenkind@gmail.com",
                            IsActief = true,
                            AantalPunten = 0,
                            LevelId=1
                        };  




                        var admin = await userManager.CreateAsync(defaultUser, "User-1");

                        if (admin.Succeeded && userManager.FindByNameAsync("UserenKind").Result != null)
                            await userManager.AddToRoleAsync(defaultUser, "gebruiker");


                        var defaultChild = new Kind
                        {
                            PersoonId=defaultUser.Id,
                            Naam = "Jansen",
                            Voornaam = "Anna",
                            Geboortedatum = new DateTime(2012, 04, 15),
                            Allergieën = "Pinda's",
                            Medicatie = "Geen"


                        };
                      
                        await _context.AddAsync(defaultChild);
                        await _context.SaveChangesAsync();

                        var defaultdeelnemer = new Deelnemer
                        {
                            KindId = defaultChild.Id,
                            GroepreisDetailsId = 3,
                            Opmerkingen="",
                            Review="",

                        };
                        await _context.AddAsync(defaultdeelnemer);
                        await _context.SaveChangesAsync();

                    }
                    if (userManager.FindByNameAsync("Monitor").Result == null)
                    {
                        var defaultUser = new CustomUser
                        {
                            Naam = "Monitor",
                            UserName = "Monitor",
                            Voornaam = "Monitor",
                            GeboorteDatum = new DateTime(25/12/2002),
                            Straat = "Antwerpsestraat",
                            Postcode = "3232",
                            Gemeente = "Lier",
                            TelefoonNummer = "014 56 44 50",
                            Huisnummer = "99",
                            Huisdokter = "Dr. Thomas",
                            Email = "monitor@gmail.com",
                            IsActief = true,
                            AantalPunten = 0,
                            LevelId=1
                        };

                        // Gebruiker aanmaken met rol
                        var admin = await userManager.CreateAsync(defaultUser, "Monitor-1");
                        if (admin.Succeeded && userManager.FindByNameAsync("Monitor").Result != null)
                            await userManager.AddToRoleAsync(defaultUser, "monitor");

                        var defaultmonitor = new Monitor
                        {
                            GroepsreisDetailsId = 1,   
                            IsHoofdMonitor=1,
                            PersoonId=defaultUser.Id,

                        };
                        await _context.AddAsync(defaultmonitor);
                        await _context.SaveChangesAsync();
                    }
                    if (userManager.FindByNameAsync("Verantwoordelijke").Result == null)
                    {
                        var defaultUser = new CustomUser
                        {
                            Naam = "Verantwoordelijke",
                            UserName = "Verantwoordelijke",
                            Voornaam = "Verantwoordelijke",
                            GeboorteDatum = new DateTime(25 / 12 / 2022),
                            Straat = "Antwerpsestraat",
                            Postcode = "3232",
                            Gemeente = "Lier",
                            TelefoonNummer = "014 56 44 50",
                            Huisnummer = "99",
                            Huisdokter = "Dr. Thomas",
                            Email = "verantwoordelijke@gmail.com",
                            IsActief = true,
                            AantalPunten = 0,
                            LevelId=1
                        };

                        // Gebruiker aanmaken met rol
                        var admin = await userManager.CreateAsync(defaultUser, "Verantwoordelijke-1");
                        if (admin.Succeeded && userManager.FindByNameAsync("Verantwoordelijke").Result != null)
                            await userManager.AddToRoleAsync(defaultUser, "verantwoordelijke");
                    }
                }
                catch (DbException ex)
                {
                    throw new Exception(ex.Message.ToString());
                }
            }
        }
    }
}
