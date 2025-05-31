using Project_ZF.ViewModels;

namespace Project_ZF.Controllers
{
    public class BestemmingController : Controller
    {
        private readonly ILogger<BestemmingController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        // Constructor Initialiseren
        public BestemmingController(IUnitOfWork unitOfWork, ILogger<BestemmingController> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        // Action methodes voor Startpagina

        public IActionResult Index()
        {
            // Haal alle bestemmingen op inclusief hun foto's en groepsreizen
            var bestemmingen = _unitOfWork.BestemmingRepository.GetAllIncluding(b => b.Fotos, b => b.Groepsreizen)
                .Select(b => new BestemmingViewModel
                {
                    Id = b.Id,
                    Code = b.Code,
                    BestemmingsNaam = b.BestemmingsNaam,
                    Beschrijving = b.Beschrijving,
                    MinLeeftijd = b.MinLeeftijd,
                    MaxLeeftijd = b.MaxLeeftijd,
                    Fotos = b.Fotos.Select(f => new FotoViewModel
                    {
                        Id = f.Id,
                        Naam = f.Naam
                    }).ToList(),
                    Groepsreizen = b.Groepsreizen.Select(g => new GroepsreisViewModel
                    {
                        Id = g.Id,
                        GroepsreisId = g.GroepsreisId,
                        BeginDatum = g.BeginDatum,
                        EindDatum = g.EindDatum,
                        Prijs = g.Prijs,
                        StandaardPunten = g.StandaardPunten,
                        
                        BestemmingsNaam = b.BestemmingsNaam,
                        Deelnemers = _unitOfWork.DeelnemerRepository.GetAllIncluding(d => d.Kind)
                            .Where(d => d.GroepreisDetailsId == g.Id)
                            .Select(d => $"{d.Kind.Voornaam} {d.Kind.Naam}")
                            .ToList(),
                        Monitoren = _unitOfWork.MonitorRepository.GetAllIncluding(m => m.CustomUser)
                            .Where(m => m.GroepsreisDetailsId == g.Id)
                            .Select(m => $"{m.CustomUser.Voornaam} {m.CustomUser.Naam}")
                            .ToList(),
                        Programmas = _unitOfWork.ProgrammaRepository.GetAllIncluding(p => p.Activiteit)
                            .Where(p => p.GroepsreisId == g.Id)
                            .Select(p => new ProgrammaViewModel
                            {
                                Id = p.Id,
                                ActiviteitNaam = p.Activiteit.Naam
                            }).ToList()
                    }).ToList()
                }).ToList();

            // Retourneer de bestemmingen aan de view
            return View(bestemmingen);
        }

        [HttpGet]
        public IActionResult Create()
        {
            // Retourneer de create view
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(BestemmingCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _logger.LogInformation("Creating new bestemming.");
                    var bestemming = new Bestemming
                    {
                        Code = model.Code,
                        BestemmingsNaam = model.BestemmingsNaam,
                        Beschrijving = model.Beschrijving,
                        MinLeeftijd = model.MinLeeftijd,
                        MaxLeeftijd = model.MaxLeeftijd,
                        Fotos = new List<Foto>()
                    };

                    if (model.Foto != null)
                    {
                        var foto = new Foto
                        {
                            Naam = model.Foto.FileName,
                            Bestemming = bestemming
                        };
                        bestemming.Fotos.Add(foto);
                    }

                    _unitOfWork.BestemmingRepository.Add(bestemming);
                    await _unitOfWork.SaveChangesAsync();

                    if (model.Foto != null)
                    {
                        var foto = bestemming.Fotos.First();
                        foto.BestemmingId = bestemming.Id; // Set the BestemmingId to the newly created Bestemming's Id
                        await SaveFotoAsync(model.Foto);
                        await _unitOfWork.SaveChangesAsync(); // Save the changes to update the Foto's BestemmingId
                    }

                    _logger.LogInformation("New bestemming created successfully.");
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Error creating bestemming: {ex.Message}");
                    ModelState.AddModelError("", "Er is een fout opgetreden bij het opslaan van de bestemming.");
                }
            }
            _logger.LogWarning("Model state is invalid.");
            return View(model);
        }

        // GET: Bestemming/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var bestemming = await _unitOfWork.BestemmingRepository.GetByIdIncludingAsync(id, b => b.Fotos);
            if (bestemming == null)
            {
                return NotFound();
            }

            var model = new BestemmingEditViewModel
            {
                Id = bestemming.Id,
                Code = bestemming.Code,
                BestemmingsNaam = bestemming.BestemmingsNaam,
                Beschrijving = bestemming.Beschrijving,
                MinLeeftijd = bestemming.MinLeeftijd,
                MaxLeeftijd = bestemming.MaxLeeftijd,
                Fotos = bestemming.Fotos.Select(f => new FotoViewModel { Id = f.Id, Naam = f.Naam }).ToList(),
                FotosToRemove = new List<FotoViewModel>()
            };

            return View(model);
        }

        // POST: Bestemming/Edit/5
        [HttpPost]
        public async Task<IActionResult> Edit(int id, BestemmingEditViewModel model, List<int> FotosToRemove)
        {
            if (id != model.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var bestemming = await _unitOfWork.BestemmingRepository.GetByIdIncludingAsync(id, b => b.Fotos);
                    if (bestemming == null)
                    {
                        return NotFound();
                    }

                    bestemming.Code = model.Code;
                    bestemming.BestemmingsNaam = model.BestemmingsNaam;
                    bestemming.Beschrijving = model.Beschrijving;
                    bestemming.MinLeeftijd = model.MinLeeftijd;
                    bestemming.MaxLeeftijd = model.MaxLeeftijd;

                    // Verwijder geselecteerde foto's
                    if (FotosToRemove != null && FotosToRemove.Any())
                    {
                        var fotosToRemove = bestemming.Fotos.Where(f => FotosToRemove.Contains(f.Id)).ToList();
                        foreach (var foto in fotosToRemove)
                        {
                            bestemming.Fotos.Remove(foto);
                            _unitOfWork.FotoRepository.Delete(foto);
                            // Voeg hier eventueel code toe om de foto van de schijf te verwijderen
                            var filePath = Path.Combine("wwwroot/images", foto.Naam);
                            if (System.IO.File.Exists(filePath))
                            {
                                System.IO.File.Delete(filePath);
                            }
                        }
                    }

                    // Voeg nieuwe foto toe
                    if (model.Foto != null)
                    {
                        var foto = new Foto
                        {
                            Naam = model.Foto.FileName,
                            Bestemming = bestemming
                        };
                        bestemming.Fotos.Add(foto);
                        await SaveFotoAsync(model.Foto);
                    }

                    _unitOfWork.BestemmingRepository.Update(bestemming);
                    await _unitOfWork.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Error updating bestemming: {ex.Message}");
                    ModelState.AddModelError("", "Er is een fout opgetreden bij het bijwerken van de bestemming.");
                }
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            // Haal de bestemming op aan de hand van het id
            var bestemming = await _unitOfWork.BestemmingRepository.GetByIdAsync(id);
            if (bestemming == null)
            {
                // Retourneer een 404 Not Found als de bestemming niet bestaat
                return NotFound();
            }

            // Maak een viewmodel aan voor de bestemming
            var model = new BestemmingViewModel
            {
                Id = bestemming.Id,
                Code = bestemming.Code,
                BestemmingsNaam = bestemming.BestemmingsNaam,
                Beschrijving = bestemming.Beschrijving,
                MinLeeftijd = bestemming.MinLeeftijd,
                MaxLeeftijd = bestemming.MaxLeeftijd,
                Fotos = bestemming.Fotos.Select(f => new FotoViewModel
                {
                    Id = f.Id,
                    Naam = f.Naam
                }).ToList()
            };

            // Retourneer de delete view met het model
            return View(model);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            // Haal de bestemming op aan de hand van het id
            var bestemming = await _unitOfWork.BestemmingRepository.GetByIdAsync(id);
            if (bestemming == null)
            {
                // Retourneer een 404 Not Found als de bestemming niet bestaat
                return NotFound();
            }

            // Verwijder gerelateerde groepsreizen
            var groepsreizen = _unitOfWork.GroepsreisRepository.GetAll(includeProperties: "Bestemming")
                .Where(gr => gr.Bestemming.Id == id).ToList();

            foreach (var groepsreis in groepsreizen)
            {
                _unitOfWork.GroepsreisRepository.Delete(groepsreis);
            }

            // Verwijder gerelateerde foto's
            foreach (var foto in bestemming.Fotos)
            {
                DeleteFoto(foto.Naam);
            }

            // Verwijder de bestemming
            _unitOfWork.BestemmingRepository.Delete(bestemming);
            await _unitOfWork.SaveChangesAsync();

            // Redirect naar de index actie
            return RedirectToAction(nameof(Index));
        }

        private async Task SaveFotoAsync(IFormFile foto)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", foto.FileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await foto.CopyToAsync(stream);
            }
        }

        private void DeleteFoto(string fileName)
        {
            // Verwijder de foto uit de wwwroot/images map
            var filePath = Path.Combine("wwwroot/images", fileName);
            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }
        }
    }
}
