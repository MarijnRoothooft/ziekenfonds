using Project_ZF.Models;
using Project_ZF.ViewModels;

public class InschrijvenGroepsreisController : Controller
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserManager<CustomUser> _userManager;

    public InschrijvenGroepsreisController(IUnitOfWork unitOfWork, UserManager<CustomUser> userManager)
    {
        _unitOfWork = unitOfWork;
        _userManager = userManager;
    }

    [HttpGet]
    public async Task<IActionResult> Inschrijven(int id)
    {
        var geregistreerdeMonitorenIds = _unitOfWork.MonitorRepository.GetAll()
            .Where(m => m.GroepsreisDetailsId == id)
            .Select(m => m.PersoonId)
            .ToList();
        var user = await _userManager.GetUserAsync(User);
        var roles = await _userManager.GetRolesAsync(user);
        var isMonitor = roles.Contains("monitor");

        // Haal de kinderen op en converteer naar KindViewModel
        var kinderen = _unitOfWork.KindRepository.GetAll()
            .Where(k => k.PersoonId == user.Id)
            .Select(k => new KindViewModel
            {
                Id = k.Id,
                PersoonId = k.PersoonId,
                Naam = k.Naam,
                Voornaam = k.Voornaam,
                Geboortedatum = k.Geboortedatum,
                Allergieën = k.Allergieën,
                Medicatie = k.Medicatie,
                CustomUser = k.CustomUser
            }).ToList();

        var groepsreis = _unitOfWork.GroepsreisRepository.GetAll(includeProperties: "Bestemming")
            .FirstOrDefault(gr => gr.Id == id);
        if (groepsreis == null)
        {
            return NotFound();
        }

        var vandaag = DateTime.Now;
        if (groepsreis.BeginDatum < vandaag.AddDays(3))
        {
            TempData["ErrorMessage"] = "De inschrijving is te laat. De reis is reeds verlopen.";

            var foto = _unitOfWork.FotoRepository.GetAll()
                .FirstOrDefault(f => f.BestemmingId == groepsreis.GroepsreisId);

            if (foto == null)
            {
                return NotFound();
            }

            return RedirectToAction("Detail", new { id = foto.Id });
        }

        // Get the list of already registered children for this trip
        var geregistreerdeKinderenIds = _unitOfWork.DeelnemerRepository.GetAll()
            .Where(d => d.GroepreisDetailsId == id)
            .Select(d => d.KindId)
            .ToList();

        var viewModel = new InschrijvenViewModel
        {
            GroepsreisId = groepsreis.Id,
            BestemmingsNaam = groepsreis.Bestemming.BestemmingsNaam,
            VertrekDatum = groepsreis.BeginDatum,
            EindDatum = groepsreis.EindDatum,
            Prijs = (decimal)groepsreis.Prijs, // Voeg expliciete cast toe hier
            StandaardPunten = groepsreis.StandaardPunten,
            Kinderen = kinderen,
            IsMonitor = isMonitor ? 1 : 0,
            GebruikerRol = string.Join(", ", roles),
            User = new CustomUser
            {
                Naam = user.Naam,
                Voornaam = user.Voornaam,
                Straat = user.Straat,
                Huisnummer = user.Huisnummer,
                Gemeente = user.Gemeente,
                Postcode = user.Postcode,
                Huisdokter = user.Huisdokter,
                Email = user.Email,
                TelefoonNummer = user.TelefoonNummer,

            },
            GeregistreerdeKinderenIds = geregistreerdeKinderenIds,
            GeregistreerdeMonitorenIds = geregistreerdeMonitorenIds
        };

        return View(viewModel);
    }
    [HttpPost]
    public async Task<IActionResult> InschrijvenKind(int GroepsreisId, int KindId)
    {
        var user = await _userManager.GetUserAsync(User);

        // Check if the child is already registered for the trip
        var isAlreadyRegistered = _unitOfWork.DeelnemerRepository.GetAll()
            .Any(d => d.GroepreisDetailsId == GroepsreisId && d.KindId == KindId);

        if (isAlreadyRegistered)
        {
            TempData["ErrorMessage"] = "Dit kind is al ingeschreven voor deze groepsreis.";
            return RedirectToAction("Inschrijven", new { id = GroepsreisId });
        }

        var groepsreis = _unitOfWork.GroepsreisRepository.GetAll(includeProperties: "Bestemming")
            .FirstOrDefault(gr => gr.Id == GroepsreisId);
        if (groepsreis == null)
        {
            TempData["ErrorMessage"] = "De groepsreis bestaat niet.";
            return RedirectToAction("Inschrijven", new { id = GroepsreisId });
        }

        // Create a new Deelnemer record
        var deelnemer = new Deelnemer
        {
            KindId = KindId,
            GroepreisDetailsId = GroepsreisId,
            Opmerkingen = string.Empty,
            ReviewScore = null,
            Review = string.Empty
        };

        _unitOfWork.DeelnemerRepository.Add(deelnemer);
        await _unitOfWork.SaveChangesAsync(); //eerst deelnemer saven zodat punten hier aan kan

        var punten = new Punten
        {
            DeelnemerId = deelnemer.Id,
            GroepsreisId = groepsreis.Id,
            AantalPunten = groepsreis.StandaardPunten,
            Datum = DateTime.Now,
            Omschrijving = $"Punten van {groepsreis.Bestemming.BestemmingsNaam}"
        };

        _unitOfWork.PuntenRepository.Add(punten);

        user.AantalPunten += groepsreis.StandaardPunten;

        var huidigLevel = _unitOfWork.LevelRepository
        .GetAll()
        .FirstOrDefault(l => l.Id == user.LevelId);

        if (huidigLevel != null)
        {
            var nieuwLevel = _unitOfWork.LevelRepository
                .GetAll()
                .Where(l => l.BenodigdePunten > huidigLevel.BenodigdePunten)
                .OrderBy(l => l.BenodigdePunten)
                .FirstOrDefault();

            if (nieuwLevel != null && user.AantalPunten >= nieuwLevel.BenodigdePunten)
            {
                user.LevelId = nieuwLevel.Id; // Verhoog het level
            }
        }

        _unitOfWork.CustomUserRepository.Update(user);

        await _unitOfWork.SaveChangesAsync();

        TempData["SuccessMessage"] = "Het kind is succesvol ingeschreven voor de groepsreis.";
        return RedirectToAction("Inschrijven", new { id = GroepsreisId });
    }

    [HttpPost]
    public async Task<IActionResult> InschrijvenMonitor(int GroepsreisId)
    {
        var user = await _userManager.GetUserAsync(User);

        // Check if the monitor is already registered for the trip
        var isAlreadyRegistered = _unitOfWork.MonitorRepository.GetAll()
            .Any(m => m.GroepsreisDetailsId == GroepsreisId && m.PersoonId == user.Id);

        if (isAlreadyRegistered)
        {
            TempData["ErrorMessage"] = "Deze monitor is al ingeschreven voor deze groepsreis.";
            return RedirectToAction("Inschrijven", new { id = GroepsreisId });
        }

        // Create a new Monitor record
        var monitor = new Project_ZF.Models.Monitor
        {
            PersoonId = user.Id,
            GroepsreisDetailsId = GroepsreisId,
            IsHoofdMonitor = 0 // or set this based on your logic
        };

        _unitOfWork.MonitorRepository.Add(monitor);
        await _unitOfWork.SaveChangesAsync();

        TempData["SuccessMessage"] = "De monitor is succesvol ingeschreven voor de groepsreis.";
        return RedirectToAction("Inschrijven", new { id = GroepsreisId });
    }
}