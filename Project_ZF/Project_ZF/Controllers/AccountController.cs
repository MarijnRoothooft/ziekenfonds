

using Microsoft.AspNetCore.Identity;
using Project_ZF.ViewModels;
using System.Security.Claims;
using System.Data;
using ErrorViewModel = Project_ZF.Models.ErrorViewModel;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;


namespace Project_ZF.Controllers
{
    public class AccountController : Controller
    {

        private readonly IUnitOfWork _context;
        private UserManager<CustomUser> _userManager;
        private SignInManager<CustomUser> _signInManager;
        private RoleManager<IdentityRole> _roleManager;


        public AccountController(IUnitOfWork context, UserManager<CustomUser> userManager, RoleManager<IdentityRole> roleManager, SignInManager<CustomUser> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;

        }

        public async Task<IActionResult> Index(AccountViewModel searchModel)
        {
            try
            {
                AccountViewModel viewModel = new AccountViewModel
                {
                    Voornaam = searchModel.Voornaam,
                    Naam = searchModel.Naam,
                    MinLeeftijd = searchModel.MinLeeftijd,
                    MaxLeeftijd = searchModel.MaxLeeftijd
                };

                // Initialize filter as always true (no filter applied yet)
                Expression<Func<CustomUser, bool>> filter = user => true;

                // Add Voornaam filter
                if (!string.IsNullOrWhiteSpace(searchModel.Voornaam))
                {
                    filter = user => user.Voornaam.Contains(searchModel.Voornaam);
                }

                // Add Naam filter
                if (!string.IsNullOrWhiteSpace(searchModel.Naam))
                {
                    filter = user => user.Naam.Contains(searchModel.Naam);
                }

                // Add MinLeeftijd and MaxLeeftijd filters
                if (searchModel.MinLeeftijd.HasValue || searchModel.MaxLeeftijd.HasValue)
                {
                    var today = DateTime.Today;

                    // Filter by minimum age
                    if (searchModel.MinLeeftijd.HasValue)
                    {
                        var minDate = today.AddYears(-searchModel.MinLeeftijd.Value);
                        filter = user => user.GeboorteDatum <= minDate;
                    }

                    // Filter by maximum age
                    if (searchModel.MaxLeeftijd.HasValue)
                    {
                        var maxDate = today.AddYears(-searchModel.MaxLeeftijd.Value);
                        filter = user => user.GeboorteDatum >= maxDate;
                    }
                }

                // Fetch accounts with filter applied
                var accounts = _context.CustomUserRepository.Find(filter);
                viewModel.Accounts = accounts.ToList();

                // Fetch roles for each account
                foreach (var account in viewModel.Accounts)
                {
                    var userRoles = await _userManager.GetRolesAsync(account);
                    viewModel.UserRoles[account.Id] = userRoles.ToList();
                }

                return View(viewModel);
            }
            catch (Exception ex)
            {
                var errorViewModel = new ErrorViewModel { Message = $"Geen accounts opgehaald: {ex.Message}" };
                return View("Error", errorViewModel);
            }
        }






        public async Task<IActionResult> Aanpassen(string id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());

            if (user == null) { return NotFound(); }

            var BeschikbareRollen = await _roleManager.Roles.Select(r => r.Name).ToListAsync();
            var huidigeRol = (await _userManager.GetRolesAsync(user)).FirstOrDefault();

            RolEditViewModel vm = new RolEditViewModel()
            {
                Naam = user.Naam,
                Email = user.Email,
                IsActief = user.IsActief,
                BeschikbareRollen = BeschikbareRollen,
                GeselecteerdeRol = huidigeRol
                
            };

            return View(vm);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Aanpassen(string id, RolEditViewModel viewModel)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                return View("Error, geen account gevonden");
            }

            var rol = await _userManager.GetRolesAsync(user);
            var BeschikbareRollen = await _roleManager.Roles.Select(r => r.Name).ToListAsync();

            var geselecteerdeRol = viewModel.GeselecteerdeRol;
            if (!rol.Contains(geselecteerdeRol))
            {
                await _userManager.RemoveFromRolesAsync(user, rol);
                await _userManager.AddToRoleAsync(user, geselecteerdeRol);
            }

            user.IsActief = viewModel.IsActief;

            _context.CustomUserRepository.Update(user);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }



    }
}
