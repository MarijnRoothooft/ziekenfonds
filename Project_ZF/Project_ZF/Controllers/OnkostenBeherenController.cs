using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Project_ZF.Data;
using Project_ZF.ViewModels;
using System.Globalization;

namespace Project_ZF.Controllers
{ 
    public class OnkostenBeherenController : Controller
    {
        private readonly ZiekenFondsContext _context;

        // SaveFotoAsync
        private async Task<string> SaveFotoAsync(IFormFile foto)
        {
            var imagesFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images");

            // Controleer of de map bestaat
            if (!Directory.Exists(imagesFolder))
            {
                Directory.CreateDirectory(imagesFolder);
            }

            // Genereer een unieke bestandsnaam
            var uniqueFileName = $"{Guid.NewGuid()}_{Path.GetFileName(foto.FileName)}";
            var filePath = Path.Combine(imagesFolder, uniqueFileName);

            // Bestand opslaan
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await foto.CopyToAsync(fileStream);
            }

            // Retourneer het relatief pad voor weergave
            return $"/Images/{uniqueFileName}";
        }


        //public IActionResult Index()
        //{
        //    return View();
        //}

        public ActionResult Index()
        {
            var onkostenLijst =  _context.Onkosten
                .Include(o => o.Groepsreis) // Laad de gekoppelde Groepsreis
                .ThenInclude(g => g.Bestemming) // Laad de gekoppelde Bestemming
                .Select(o => new OnkostenViewModel
                {
                    Id = o.Id,
                    Titel = o.Titel,
                    Omschrijving = o.Omschrijving,
                    Bedrag = o.Bedrag,
                    Datum = o.Datum,
                    Foto = o.Foto,
                    BestemmingsNaam = o.Groepsreis.Bestemming.BestemmingsNaam // Haal de naam van de bestemming op
                })
                .ToList();


            foreach (var onkosten in onkostenLijst)
            {
                Console.WriteLine($"Id: {onkosten.Id}, Bestemming: {onkosten.BestemmingsNaam}");
            }

            return View(onkostenLijst);
        }




        public OnkostenBeherenController(ZiekenFondsContext context)
        {
            _context = context;
        }

        // GET: OnkostenBeheren/Edit
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var onkosten = await _context.Onkosten.FindAsync(id);
            if (onkosten == null)
            {
                return NotFound();
            }

            // Lijst van bestemmingen doorgeven, gesorteerd alfabetisch
            ViewBag.Bestemmingen = _context.Bestemmingen
                .OrderBy(b => b.BestemmingsNaam)
                .Select(b => new SelectListItem
                {
                    Value = b.Id.ToString(),
                    Text = b.BestemmingsNaam
                }).ToList();

            return View(onkosten);
        }


        // POST: OnkostenBeheren/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Onkosten model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(model);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OnkostenExists(model.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            // Bestemmingen opnieuw laden bij fout
            ViewBag.Bestemmingen = _context.Bestemmingen
                .OrderBy(b => b.BestemmingsNaam)
                .Select(b => new SelectListItem
                {
                    Value = b.Id.ToString(),
                    Text = b.BestemmingsNaam
                }).ToList();

            return View(model);
        }

        private bool OnkostenExists(int id)
        {
            return _context.Onkosten.Any(e => e.Id == id);
        }




        // GET: OnkostenBeheren/Details
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Haal het onkosten-item op inclusief de bijbehorende Bestemmingsnaam
            var onkosten = await _context.Onkosten
                .Include(o => o.Groepsreis)
                .ThenInclude(g => g.Bestemming)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (onkosten == null)
            {
                return NotFound();
            }

            // Maak een ViewModel of anoniem object met de bestemmingsnaam
            var viewModel = new
            {
                onkosten.Id,
                onkosten.GroepsreisId,
                BestemmingsNaam = onkosten.Groepsreis?.Bestemming?.BestemmingsNaam ?? "Geen bestemming",
                onkosten.Titel,
                onkosten.Omschrijving,
                onkosten.Bedrag,
                onkosten.Datum,
                onkosten.Foto
            };

            return View(viewModel);
        }



        // Get: OnkostenBeheren/Create 
        public IActionResult Create()
                {
                    // Haal bestemmingen op en sorteer alfabetisch
                    ViewBag.Bestemmingen = _context.Bestemmingen
                        .OrderBy(b => b.BestemmingsNaam) // Sorteer alfabetisch
                        .Select(b => new SelectListItem
                        {
                            Value = b.Id.ToString(),
                            Text = b.BestemmingsNaam
                        })
                        .ToList();

                    return View();
                }


        // POST: OnkostenBeheren/Create 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(OnkostenCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var onkosten = new Onkosten
                {
                    GroepsreisId = model.GroepsreisId,
                    Titel = model.Titel,
                    Omschrijving = model.Omschrijving,
                    Bedrag = model.Bedrag,
                    Datum = model.Datum
                };

                // Foto verwerken en opslaan
                if (model.Foto != null && model.Foto.Length > 0)
                {
                    var fotoPad = await SaveFotoAsync(model.Foto);
                    onkosten.Foto = fotoPad; // Pad opslaan in database
                }

                _context.Add(onkosten);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // Herladen van bestemmingen bij een fout
            ViewBag.Bestemmingen = _context.Bestemmingen
                .Select(b => new SelectListItem { Value = b.Id.ToString(), Text = b.BestemmingsNaam })
                .ToList();

            return View(model);
        }

        // GET: OnkostenBeheren/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var onkosten = await _context.Onkosten
                .Include(o => o.Groepsreis)
                .ThenInclude(g => g.Bestemming) // Laad de bestemming
                .FirstOrDefaultAsync(o => o.Id == id);

            if (onkosten == null)
            {
                return NotFound();
            }

            return View(onkosten);
        }

        // POST: OnkostenBeheren/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var onkosten = await _context.Onkosten.FindAsync(id);
            if (onkosten != null)
            {
                _context.Onkosten.Remove(onkosten);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
