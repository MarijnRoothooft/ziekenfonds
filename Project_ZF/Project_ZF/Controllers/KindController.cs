using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project_ZF.ViewModels;

namespace Project_ZF.Controllers
{
    public class KindController: Controller
    {
        private readonly IUnitOfWork _context;
        private readonly UserManager<CustomUser> _userManager;

        public KindController(IUnitOfWork context, UserManager<CustomUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index(string id)
        {
            try
            {
                var user = await _userManager.Users
                    .Include(u => u.Kinderen)
                    .FirstOrDefaultAsync(u => u.Id == id);

                if (user == null)
                {
                    return NotFound();
                }

                var kindViewModels = user.Kinderen.Select(k => new KindViewModel
                {
                    Id = k.Id,
                    PersoonId = k.PersoonId,
                    Naam = k.Naam,
                    Voornaam = k.Voornaam,
                    Geboortedatum = k.Geboortedatum,
                    Allergieën = k.Allergieën,
                    Medicatie = k.Medicatie
                }).ToList();

                return View(kindViewModels);
            }
            catch (Exception ex)
            {
                var errorViewModel = new ErrorViewModel { Message = $"Geen kinderen opgehaald.: {ex.Message}" };
                return View("Error", errorViewModel);
            }
        }

        public IActionResult Toevoegen()
        {
            return View("Toevoegen");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Toevoegen(KindToevoegenViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var user = await _userManager.GetUserAsync(User);
                    if (user == null) { return NotFound(); }

                    var kind = new Kind()
                    {
                        Naam = viewModel.Naam,
                        Voornaam = viewModel.Voornaam,
                        Geboortedatum = viewModel.Geboortedatum,
                        Allergieën = viewModel.Allergieën,
                        Medicatie = viewModel.Medicatie,
                        PersoonId = user.Id
                    };

                    await _context.KindRepository.AddAsync(kind);
                    await _context.SaveChangesAsync();

                    return RedirectToAction("Index", new { id = user.Id });

                }
                catch
                {
                    return View("Error");
                }
            }
            else
            {
                return View(viewModel);
            }  
        }

        public async Task<IActionResult> Wijzigen(int id)
        {
            var kind = await _context.KindRepository.GetByIdAsync(id);

            if (kind == null) { return NotFound(); }

            KindWijzigenViewModel vm = new KindWijzigenViewModel()
            {
                Id = kind.Id,
                Naam = kind.Naam,
                Voornaam = kind.Voornaam,
                Geboortedatum = kind.Geboortedatum,
                Allergieën = kind.Allergieën,
                Medicatie = kind.Medicatie

            };

            return View(vm);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task <IActionResult> Wijzigen(int id, KindWijzigenViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            try
            {
                var kind = await _context.KindRepository.GetByIdAsync(id);
                if (kind == null)
                {
                    return NotFound();
                }

                kind.Naam = vm.Naam;
                kind.Voornaam = vm.Voornaam;
                kind.Geboortedatum = vm.Geboortedatum;
                kind.Allergieën = vm.Allergieën;
                kind.Medicatie = vm.Medicatie;

                _context.KindRepository.Update(kind);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index", new { id = kind.PersoonId });
            }
            catch (Exception ex)
            {
                var errorViewModel = new ErrorViewModel { Message = $"Fout bij het bijwerken van kind: {ex.Message}" };
                return View("Error", errorViewModel);
            }
        }

        [HttpPost, ActionName("Verwijderen")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>Verwijderen(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            var kind = await _context.KindRepository.GetByIdAsync(id);

            try
            {
                if (kind != null)
                {
                    _context.KindRepository.Delete(kind);
                   await _context.SaveChangesAsync();
                }
                else
                {
                    throw new Exception($"{id} niet gevonden");
                }

                return RedirectToAction("Index", new {id = user.Id});
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return View("Error");
            }
        }
    }
}
