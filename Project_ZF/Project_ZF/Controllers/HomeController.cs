using Project_ZF.ViewModels;
using System.Diagnostics;

namespace Project_ZF.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private UserManager<CustomUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;
        private SignInManager<CustomUser> _signInManager;

        // Constructor Initialiseren 
        public HomeController(ILogger<HomeController> logger, UserManager<CustomUser> userManager, RoleManager<IdentityRole> roleManager, SignInManager<CustomUser> signInManager, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _unitOfWork = unitOfWork;
        }

        // Actiemethode om de startpagina weer te geven
        public IActionResult Index(DateTime? begindatum, DateTime? einddatum, int? minLeeftijd, int? maxLeeftijd)
        {
            // Haal alle groepsreizen op inclusief bestemmingen en programma's
            var groepsreizen = _unitOfWork.GroepsreisRepository.GetAll(includeProperties: "Bestemming,Programmas.Activiteit").ToList();

            // Filter de groepsreizen op basis van de begin- en einddatum
            var vandaag = DateTime.Now;
            var beschikbareGroepsreizen = groepsreizen.Where(gr => gr.BeginDatum >= vandaag).ToList();

            if (begindatum.HasValue)
            {
                beschikbareGroepsreizen = beschikbareGroepsreizen.Where(gr => gr.BeginDatum >= begindatum.Value).ToList();
            }

            if (einddatum.HasValue)
            {
                beschikbareGroepsreizen = beschikbareGroepsreizen.Where(gr => gr.EindDatum <= einddatum.Value).ToList();
            }

            if (minLeeftijd.HasValue)
            {
                beschikbareGroepsreizen = beschikbareGroepsreizen.Where(gr => gr.Bestemming.MinLeeftijd >= minLeeftijd.Value).ToList();
            }

            if (maxLeeftijd.HasValue)
            {
                beschikbareGroepsreizen = beschikbareGroepsreizen.Where(gr => gr.Bestemming.MaxLeeftijd <= maxLeeftijd.Value).ToList();
            }

            var verlopenGroepsreizen = groepsreizen.Where(gr => gr.EindDatum < vandaag).ToList();

            // Haal alle foto's op inclusief bestemmingen
            var fotos = _unitOfWork.FotoRepository.GetAll(includeProperties: "Bestemming").ToList();

            // Maak een nieuw HomePageViewModel en vul deze met de beschikbare en verlopen groepsreizen
            var viewModel = new HomePageViewModel
            {
                BeschikbareGroepsreizen = beschikbareGroepsreizen,
                VerlopenGroepsreizen = verlopenGroepsreizen,
                BeschikbareFotos = fotos
            };

            return View(viewModel);
        }

        // Actiemethode om de foutpagina weer te geven
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            // Maak een nieuw ErrorViewModel aan met het huidige request ID
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}