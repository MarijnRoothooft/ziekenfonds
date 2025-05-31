using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Project_ZF.Data;
using Project_ZF.ViewModels;
using System.Diagnostics;

namespace Project_ZF.Controllers
{
    public class OpleidingController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<OpleidingController> _logger;
        private readonly ZiekenFondsContext _context;

        public OpleidingController(IUnitOfWork unitOfWork, ILogger<OpleidingController> logger, ZiekenFondsContext context)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _context = context;
        }

        // GET: Opleiding
        public async Task<IActionResult> Index()
        {
            var opleidingen = await _unitOfWork.OpleidingRepository.GetAllAsync();
            var viewModel = opleidingen.Select(o => new OpleidingViewModel
            {
                Id = o.Id,
                Naam = o.Naam,
                Beschrijving = o.Beschrijving,
                BeginDatum = o.BeginDatum,
                EindDatum = o.EindDatum,
                AantalPlaatsen = o.AantalPlaatsen,
                OpleidingVereistId = o.OpleidingVereistId,
                VereisteOpleidingen = o.VereisteOpleidingen
            }).ToList();

            return View(viewModel);
        }

        // GET: Opleiding/Create
        public IActionResult Create()
        {
            return View();
        }

        private IEnumerable<SelectListItem> GetSelectListItems(IEnumerable<string> elements)
        {
            // Create an empty list to hold result of the operation
            var selectList = new List<SelectListItem>();

            // For each string in the 'elements' variable, create a new SelectListItem object
            // that has both its Value and Text properties set to a particular value.
            // This will result in MVC rendering each item as:
            //     <option value="State Name">State Name</option>
            foreach (var element in elements)
            {
                selectList.Add(new SelectListItem
                {
                    Value = element,
                    Text = element
                });
            }

            return selectList;
        }

        // POST: Opleiding/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(OpleidingViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var opleiding = new Opleiding
                    {
                        Naam = model.Naam,
                        Beschrijving = model.Beschrijving,
                        BeginDatum = model.BeginDatum,
                        EindDatum = model.EindDatum,
                        AantalPlaatsen = model.AantalPlaatsen,
                        OpleidingVereistId = model.AangeduiddeOpleiding.Id,
                };

                    await _unitOfWork.OpleidingRepository.AddAsync(opleiding);
                    await _unitOfWork.SaveChangesAsync();

                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Er is een fout opgetreden bij het toevoegen van de opleiding.");
                    ModelState.AddModelError("", "Er is een fout opgetreden bij het toevoegen van de opleiding.");
                }
            }

            return View(model);
        }

        // Actiemethode om de foutpagina weer te geven
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        // GET: Opleiding/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var opleiding = await _unitOfWork.OpleidingRepository.GetByIdAsync(id.Value);
            if (opleiding == null)
            {
                return NotFound();
            }

            var viewModel = new OpleidingViewModel
            {
                Id = opleiding.Id,
                Naam = opleiding.Naam,
                Beschrijving = opleiding.Beschrijving,
                BeginDatum = opleiding.BeginDatum,
                EindDatum = opleiding.EindDatum,
                AantalPlaatsen = opleiding.AantalPlaatsen,
                OpleidingVereistId = opleiding.OpleidingVereistId
            };

            return View(viewModel);
        }

        // POST: Opleiding/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, OpleidingViewModel model)
        {
            // Verwijder VereisteOpleidingNaam uit de ModelState
            ModelState.Remove("VereisteOpleidingNaam");

            if (id != model.Id)
            {
                return NotFound();
            }

            // IsValid is het probleem dit komt door onze nieuw opleidingViewModel omdat deze nu een  public string VereisteOpleidingNaam { get; set; } heeft zoek dit morgen verder uit

            if (ModelState.IsValid)
            {
                try
                {
                    var opleiding = await _unitOfWork.OpleidingRepository.GetByIdAsync(id);
                    if (opleiding == null)
                    {
                        return NotFound();
                    }

                    opleiding.Naam = model.Naam;
                    opleiding.Beschrijving = model.Beschrijving;
                    opleiding.BeginDatum = model.BeginDatum;
                    opleiding.EindDatum = model.EindDatum;
                    opleiding.AantalPlaatsen = model.AantalPlaatsen;
                    opleiding.OpleidingVereistId = model.OpleidingVereistId;

                    _unitOfWork.OpleidingRepository.Update(opleiding);
                    await _unitOfWork.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Er is een fout opgetreden bij het bewerken van de opleiding.");
                    ModelState.AddModelError("", "Er is een fout opgetreden bij het bewerken van de opleiding.");
                }
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var opleiding = await _unitOfWork.OpleidingRepository.GetByIdAsync(id.Value);
            if (opleiding == null)
            {
                return NotFound();
            }

            var viewModel = new OpleidingViewModel
            {
                Id = opleiding.Id,
                Naam = opleiding.Naam,
                Beschrijving = opleiding.Beschrijving,
                BeginDatum = opleiding.BeginDatum,
                EindDatum = opleiding.EindDatum,
                AantalPlaatsen = opleiding.AantalPlaatsen,
                OpleidingVereistId = opleiding.OpleidingVereistId
            };

            return View(viewModel);
        }

        // GET: Opleiding/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var opleiding = await _unitOfWork.OpleidingRepository.GetByIdAsync(id.Value);
            if (opleiding == null)
            {
                return NotFound();
            }

            var viewModel = new OpleidingViewModel
            {
                Id = opleiding.Id,
                Naam = opleiding.Naam,
                Beschrijving = opleiding.Beschrijving,
                BeginDatum = opleiding.BeginDatum,
                EindDatum = opleiding.EindDatum,
                AantalPlaatsen = opleiding.AantalPlaatsen,
                OpleidingVereistId = opleiding.OpleidingVereistId
            };

            return View(viewModel);
        }

        // POST: Opleiding/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var opleiding = await _unitOfWork.OpleidingRepository.GetByIdAsync(id);
            if (opleiding == null)
            {
                return NotFound();
            }

            try
            {
                _unitOfWork.OpleidingRepository.Delete(opleiding);
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Er is een fout opgetreden bij het verwijderen van de opleiding.");
                ModelState.AddModelError("", "Er is een fout opgetreden bij het verwijderen van de opleiding.");
                return View(opleiding);
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Opleiding/Booking
        public async Task<IActionResult> Booking(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Haal de huidige opleiding op
            var huidigeOpleiding = await _context.Opleidingen.FirstOrDefaultAsync(o => o.Id == id.Value);

            if (huidigeOpleiding == null)
            {
                return NotFound();
            }

            // Controleer of er een OpleidingVereistId is en bepaal de vereiste opleiding
            string vereisteOpleidingNaam;
            if (huidigeOpleiding.OpleidingVereistId.HasValue)
            {
                // Zoek de opleiding met het overeenkomende ID
                var vereisteOpleiding = await _context.Opleidingen
                    .FirstOrDefaultAsync(o => o.Id == huidigeOpleiding.OpleidingVereistId.Value);

                // Als een bijpassende opleiding bestaat, haal de naam op
                vereisteOpleidingNaam = vereisteOpleiding?.Naam ?? "Geen vereiste opleidingen";
            }
            else
            {
                // Als OpleidingVereistId null is
                vereisteOpleidingNaam = "Geen vereiste opleidingen";
            }

            // Vul het ViewModel
            var viewModel = new OpleidingViewModel
            {
                Id = huidigeOpleiding.Id,
                Naam = huidigeOpleiding.Naam,
                Beschrijving = huidigeOpleiding.Beschrijving,
                BeginDatum = huidigeOpleiding.BeginDatum,
                EindDatum = huidigeOpleiding.EindDatum,
                AantalPlaatsen = huidigeOpleiding.AantalPlaatsen,
                OpleidingVereistId = huidigeOpleiding.OpleidingVereistId,
                VereisteOpleidingNaam = vereisteOpleidingNaam // Vul correct de naam van de vereiste opleiding in
            };

            return View(viewModel);
        }



        // POST: Opleiding/Booking
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BookingPost(int id, OpleidingViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Haal de opleiding op
                    var opleiding = await _context.Opleidingen.FirstOrDefaultAsync(o => o.Id == id);
                    if (opleiding == null)
                    {
                        return NotFound();
                    }

                    // Controleer of het aantal geboekte plaatsen binnen de limiet ligt
                    if (model.AantalPlaatsen > opleiding.AantalPlaatsen)
                    {
                        ModelState.AddModelError("", "Het aantal geboekte plaatsen mag niet hoger zijn dan het beschikbare aantal.");
                        return View(model);
                    }

                    // Update het aantal beschikbare plaatsen
                    opleiding.AantalPlaatsen -= model.AantalPlaatsen;
                    _context.Opleidingen.Update(opleiding);
                    await _context.SaveChangesAsync();

                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Er is een fout opgetreden bij het boeken van de opleiding.");
                    ModelState.AddModelError("", "Er is een fout opgetreden bij het verwerken van je aanvraag.");
                }
            }

            return View(model);
        }
    
    }
}
