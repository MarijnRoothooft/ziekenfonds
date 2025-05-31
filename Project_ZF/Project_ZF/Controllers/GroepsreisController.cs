using Project_ZF.ViewModels;
using System.Linq.Expressions;

namespace Project_ZF.Controllers
{
    public class GroepsreisController : Controller
    {
        private readonly ILogger<GroepsreisController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public GroepsreisController(IUnitOfWork unitOfWork, ILogger<GroepsreisController> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        // GET: Groepsreis/AddGroepsreis
        [HttpGet]
        public async Task<IActionResult> AddGroepsreis(int id)
        {
            var bestemming = await _unitOfWork.BestemmingRepository.GetByIdAsync(id);
            if (bestemming == null)
            {
                return NotFound();
            }
 
            var model = new GroepsreisCreateViewModel
            {
                GroepsreisId = bestemming.Id,
                BestemmingsNaam = bestemming.BestemmingsNaam,
                
            };

            return View(model);
        }

        // POST: Groepsreis/AddGroepsreis
        [HttpPost]
        public async Task<IActionResult> AddGroepsreis(GroepsreisCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var bestemming = await _unitOfWork.BestemmingRepository.GetByIdAsync(model.GroepsreisId);
                    if (bestemming == null)
                    {
                        return NotFound();
                    }

                    var groepsreis = new Groepsreis
                    {
                        BeginDatum = model.BeginDatum,
                        EindDatum = model.EindDatum,
                        Prijs = model.Prijs,
                        StandaardPunten = model.StandaardPunten,
                        GroepsreisId = model.GroepsreisId,
                        Bestemming = bestemming
                    };

                    bestemming.Groepsreizen.Add(groepsreis);
                    _unitOfWork.BestemmingRepository.Update(bestemming);
                    await _unitOfWork.SaveChangesAsync();

                    return RedirectToAction("Index", "Bestemming");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error adding groepsreis");
                    ModelState.AddModelError("", "Er is een fout opgetreden bij het toevoegen van de groepsreis.");
                }
            }
            return View(model);
        }

        // GET: Groepsreis/EditGroepsreis
        [HttpGet]
        public async Task<IActionResult> EditGroepsreis(int id)
        {
            var groepsreis = await _unitOfWork.GroepsreisRepository.GetByIdAsync(id);
            if (groepsreis == null)
            {
                return NotFound();
            }

            var bestemming = await _unitOfWork.BestemmingRepository.GetByIdAsync(groepsreis.GroepsreisId);
            if (bestemming == null)
            {
                return NotFound();
            }

            var model = new GroepsreisEditViewModel
            {
                Id = groepsreis.Id,
                GroepsreisId = bestemming.Id,
                BeginDatum = groepsreis.BeginDatum,
                EindDatum = groepsreis.EindDatum,
                Prijs = groepsreis.Prijs,
                StandaardPunten = groepsreis.StandaardPunten,
                BestemmingsNaam = bestemming.BestemmingsNaam
            };

            return View(model);
        }

        // POST: Groepsreis/EditGroepsreis
        [HttpPost]
        public async Task<IActionResult> EditGroepsreis(GroepsreisEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var groepsreis = await _unitOfWork.GroepsreisRepository.GetByIdAsync(model.Id);
                    if (groepsreis == null)
                    {
                        return NotFound();
                    }

                    groepsreis.BeginDatum = model.BeginDatum;
                    groepsreis.EindDatum = model.EindDatum;
                    groepsreis.Prijs = model.Prijs;
                    groepsreis.StandaardPunten = model.StandaardPunten;

                    _unitOfWork.GroepsreisRepository.Update(groepsreis);
                    await _unitOfWork.SaveChangesAsync();

                    return RedirectToAction("Index", "Bestemming");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error editing groepsreis");
                    ModelState.AddModelError("", "Er is een fout opgetreden bij het aanpassen van de groepsreis.");
                }
            }
            return View(model);
        }

        // DELETE: Groepsreis/DeleteGroepsreis
        [HttpPost]
        public async Task<IActionResult> DeleteGroepsreis(int id)
        {
            try
            {
                var groepsreis = await _unitOfWork.GroepsreisRepository.GetByIdAsync(id);
                if (groepsreis == null)
                {
                    return NotFound();
                }

                // Check of de groepsreis deelnemers of monitoren heeft 
                if (groepsreis.Deelnemers.Any() || groepsreis.Monitoren.Any())
                {
                    ModelState.AddModelError("", "De groepsreis kan niet worden verwijderd omdat er deelnemers of monitors zijn.");
                    return RedirectToAction("Index", "Bestemming");
                }

                _unitOfWork.GroepsreisRepository.Delete(groepsreis);
                await _unitOfWork.SaveChangesAsync();

                return RedirectToAction("Index", "Bestemming");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting groepsreis");
                ModelState.AddModelError("", "Er is een fout opgetreden bij het verwijderen van de groepsreis.");
                return RedirectToAction("Index", "Bestemming");
            }
        }

        public async Task<IEnumerable<Punten>> GetPunten(int id)
        {

            Expression<Func<Punten, bool>> filter = g => true && g.GroepsreisId == id;
            var punten = await _unitOfWork.PuntenRepository.GetWithIncludesAsync(
                filter

                );

            return punten;

        }

    }


}

    
