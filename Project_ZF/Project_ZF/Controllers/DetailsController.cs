using Project_ZF.ViewModels;

namespace Project_ZF.Controllers
{
    public class DetailsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<CustomUser> _userManager;

        public DetailsController(IUnitOfWork unitOfWork, UserManager<CustomUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public async Task<IActionResult> Detail(int id)
        {
            // Haal de groepsreis op met de meegegeven id
            var groepsreis = _unitOfWork.GroepsreisRepository.GetAll(includeProperties: "Bestemming,Programmas,Programmas.Activiteit")
                .FirstOrDefault(gr => gr.Id == id);

            // Als de groepsreis niet gevonden wordt, geef een 404 Not Found terug
            if (groepsreis == null)
            {
                return NotFound();
            }

            // Haal de foto op die gekoppeld is aan de bestemming van de groepsreis
            var foto = _unitOfWork.FotoRepository.GetAll(includeProperties: "Bestemming")
                .FirstOrDefault(f => f.Bestemming.Id == groepsreis.GroepsreisId);

            // Als de foto niet gevonden wordt, geef een 404 Not Found terug
            if (foto == null)
            {
                return NotFound();
            }

            // Maak een lijst van activiteiten gekoppeld aan de programma's van de groepsreis
            var activiteiten = groepsreis.Programmas.Select(p => new ActiviteitViewModel
            {
                Naam = p.Activiteit.Naam,
                Beschrijving = p.Activiteit.Beschrijving
            }).ToList();

            // Haal de reviews op die gekoppeld zijn aan de groepsreis en een review score hebben
            var reviews = _unitOfWork.DeelnemerRepository.GetAll(includeProperties: "Kind")
                .Where(d => d.Groepsreis.Id == groepsreis.Id && d.ReviewScore.HasValue)
                .Select(d => new ReviewViewModel
                {
                    ReviewScore = d.ReviewScore.Value,
                    Review = d.Review,
                    Opmerkingen = d.Opmerkingen,
                    KindNaam = d.Kind.Naam,
                    DeelnemerId = d.Id,
                    KindId = d.KindId
                }).ToList();

            // Bereken de gemiddelde score van de reviews
            double averageScore = reviews.Any() ? reviews.Average(r => r.ReviewScore) : 0;

            // Maak een viewmodel aan met alle benodigde gegevens
            var viewModel = new DetailsViewModel
            {
                Id = foto.Id,
                Naam = foto.Naam,
                BestemmingsNaam = foto.Bestemming.BestemmingsNaam,
                Beschrijving = foto.Bestemming.Beschrijving,
                MinLeeftijd = foto.Bestemming.MinLeeftijd,
                MaxLeeftijd = foto.Bestemming.MaxLeeftijd,
                Fotos = new List<Foto> { foto },
                Activiteiten = activiteiten,
                VertrekDatum = groepsreis.BeginDatum,
                EindDatum = groepsreis.EindDatum,
                Prijs = groepsreis.Prijs,
                StandaardPunten = groepsreis.StandaardPunten,
                GroepsreisId = groepsreis.Id,
                EerdereReviews = reviews,
                AverageScore = averageScore,
                HeeftKind = false // Default to false
            };

            // Haal de huidige gebruiker op
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser != null)
            {
                // Haal het kind op dat gekoppeld is aan de huidige gebruiker
                var kind = _unitOfWork.KindRepository.GetAll()
                    .FirstOrDefault(k => k.PersoonId == currentUser.Id);

                if (kind != null)
                {
                    // Haal de deelnemer op die gekoppeld is aan het kind en de groepsreis
                    var deelnemer = _unitOfWork.DeelnemerRepository.GetAll()
                        .FirstOrDefault(d => d.Groepsreis.Id == groepsreis.Id);

                    viewModel.DeelnemerId = deelnemer?.Id ?? 0;
                    viewModel.HeeftKind = true; // Set to true if the user has a child
                }
            }

            // Geef het viewmodel terug aan de view
            return View(viewModel);
        }



        // Action method AddReview en DeleteReview
        [HttpPost]
        public async Task<IActionResult> AddReview(ReviewCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var currentUser = await _userManager.GetUserAsync(User);
                if (currentUser == null)
                {
                    return Unauthorized();
                }

                var kind = _unitOfWork.KindRepository.GetAll()
                    .FirstOrDefault(k => k.PersoonId == currentUser.Id);

                if (kind == null)
                {
                    return NotFound();
                }

                var deelnemer = _unitOfWork.DeelnemerRepository.GetAll()
                    .FirstOrDefault(d => d.KindId == kind.Id && d.GroepreisDetailsId == model.GroepsreisId);

                if (deelnemer == null)
                {
                    // Create a new participant with the review details
                    deelnemer = new Deelnemer
                    {
                        KindId = kind.Id,
                        GroepreisDetailsId = model.GroepsreisId,
                        ReviewScore = model.ReviewScore,
                        Review = model.Review,
                        Opmerkingen = model.Opmerkingen
                    };
                    await _unitOfWork.DeelnemerRepository.AddAsync(deelnemer);
                }
                else
                {
                    // Update the existing participant's review details
                    deelnemer.ReviewScore = model.ReviewScore;
                    deelnemer.Review = model.Review;
                    deelnemer.Opmerkingen = model.Opmerkingen;
                    _unitOfWork.DeelnemerRepository.Update(deelnemer);
                }

                await _unitOfWork.SaveChangesAsync();

                return RedirectToAction("Detail", new { id = model.FotoId });
            }

            return View("Detail", model);
        }

        // Action method to delete a review
        [HttpPost]
        public async Task<IActionResult> DeleteReview(ReviewCreateViewModel model)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return Unauthorized();
            }

            var kind = _unitOfWork.KindRepository.GetAll()
                .FirstOrDefault(k => k.PersoonId == currentUser.Id);

            if (kind == null)
            {
                return NotFound();
            }

            var deelnemer = _unitOfWork.DeelnemerRepository.GetAll()
                .FirstOrDefault(d => d.Id == model.DeelnemerId && d.KindId == kind.Id && d.GroepreisDetailsId == model.GroepsreisId);

            if (deelnemer == null)
            {
                return NotFound();
            }

            // Delete enkel de deelnemer zijn/haar review
            _unitOfWork.DeelnemerRepository.Delete(deelnemer);
            await _unitOfWork.SaveChangesAsync();

            return RedirectToAction("Detail", new { id = model.FotoId });
        }
    }
}

