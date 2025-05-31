using System.ComponentModel.DataAnnotations;

namespace Project_ZF.ViewModels
{
    public class MonitorBeheerViewModel
    { 

            public MonitorBeheerViewModel()
            {
                UserRoles = new Dictionary<string, List<string>>();
                //Accounts = new List<CustomUser>();
                //Monitors = new List<Models.Monitor>();
                Kinderen = new List<Kind>();
                OpleidingsPersonen = new List<OpleidingPersoon>();
            }

            [Required(ErrorMessage = "Voornaam?")]
            [RegularExpression(@"^[a-zA-Z\s]*$", ErrorMessage = "Alleen letters en spaties zijn toegestaan.")]
            public string Voornaam { get; set; }

            [Required(ErrorMessage = "Naam?")]
            [RegularExpression(@"^[a-zA-Z\s]*$", ErrorMessage = "Alleen letters en spaties zijn toegestaan.")]
            public string Naam { get; set; }

            /*[Required(ErrorMessage = "Straat?")]
            public string Straat { get; set; }

            [Required(ErrorMessage = "Huisnummer?")]
            [RegularExpression(@"^\d+$", ErrorMessage = "Alleen getallen zijn toegestaan.")]
            public string Huisnummer { get; set; }

            [Required(ErrorMessage = "Gemeente?")]
            public string Gemeente { get; set; }

            [Required(ErrorMessage = "Postcode?")]
            [RegularExpression(@"^\d+$", ErrorMessage = "Alleen getallen zijn toegestaan.")]
            public string PostCode { get; set; }

            [Required(ErrorMessage = "Geboortedatum?.")]
            [DataType(DataType.Date, ErrorMessage = "Ongeldige datum.")]
            public DateTime Geboortedatum { get; set; }

            public string Huisdokter { get; set; }

            [Required(ErrorMessage = "E-mail?.")]
            [EmailAddress(ErrorMessage = "Ongeldig e-mailadres.")]
            public string Email { get; set; }

            [Required(ErrorMessage = "Telefoonnummer?")]
            [Phone(ErrorMessage = "Ongeldig telefoonnummer.")]
            public string TelefoonNummer { get; set; }

            [Required(ErrorMessage = "Rekeningnummer?")]
            [RegularExpression(@"^[a-zA-Z0-9]*$", ErrorMessage = "Alleen letterss en cijfers zijn toegestaan.")]
            public string RekeningNummer { get; set; }*/

            public bool IsActief { get; set; }
            public bool IsHoofdMonitor { get; set; }
       


            public Dictionary<string, List<string>> UserRoles { get; set; }

            public List<CustomUser> Users { get; set; } 
            public List<CustomUser> Monitoren { get; set; }
            //public List<Models.Monitor> Monitors { get; set; }
            public List<Kind> Kinderen { get; set; }
            public List<OpleidingPersoon> OpleidingsPersonen { get; set; }
            public object Id { get; internal set; }

            public int BerekenLeeftijd(DateTime geboortedatum)
            {
                var vandaag = DateTime.Today;
                var leeftijd = vandaag.Year - geboortedatum.Year;

                if (geboortedatum.Date > vandaag.AddYears(-leeftijd)) leeftijd--;

                return leeftijd;
            }
        }


 }



