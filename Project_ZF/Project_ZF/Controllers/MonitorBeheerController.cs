using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Project_ZF.ViewModels;
using System.Linq.Expressions;

namespace Project_ZF.Controllers
{
    public class MonitorBeheerController : Controller
    {
        private readonly IUnitOfWork _context;
        private UserManager<CustomUser> _userManager;
        private SignInManager<CustomUser> _signInManager;
        private RoleManager<IdentityRole> _roleManager;
        public bool hoofdMonitor;


        public MonitorBeheerController(IUnitOfWork context, UserManager<CustomUser> userManager, RoleManager<IdentityRole> roleManager, SignInManager<CustomUser> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;

        }
        [Authorize(Roles = "beheerder,verantwoordelijke,admin")]

        public async Task<IActionResult> Index(MonitorBeheerViewModel vm)
        {
            try
            {
                var users = await _context.CustomUserRepository.GetAllAsync();
                vm.Users = users.ToList();

                var monitoren = new List<CustomUser>();

                foreach (var user in users)
                {
                    var userRoles = await _userManager.GetRolesAsync(user);

                    if (userRoles.Contains("monitor"))
                    {  
                       monitoren.Add(user);
                    }
                }


                vm.Monitoren = monitoren;

                return View(vm);

            }
            catch (Exception ex)
            {
                var errorViewModel = new ErrorViewModel { Message = $"Geen accounts opgehaald.: {ex}" };
                return View("Error", errorViewModel);
            }

        }
        public async Task<IActionResult> Bewerken(string id)
        {

            var monitor = await _userManager.FindByIdAsync(id.ToString());

            if (monitor == null) { return NotFound(); }

            //var monitorRol = await _userManager.GetRolesAsync(monitor);

            MonitorEditViewModel vm = new MonitorEditViewModel()
            {
                Naam = monitor.Naam,
                Voornaam = monitor.Voornaam,
                Email = monitor.Email,
                IsActief = monitor.IsActief,
                IsHoofdMonitor = monitor.IsHoofdMonitor
            };

            return View(vm);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Bewerken(string id, MonitorEditViewModel vm)
        {
            var monitor = await _userManager.FindByIdAsync(id);

            if (monitor == null)
            {
                return View("Error, geen account gevonden");
            }

            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            //var rol = await _userManager.GetRolesAsync(monitor);
            /*if (vm.IsHoofdMonitor)
            {
               monitor.IsHoofdMonitor = true;
            }
            else
            {
                monitor.IsHoofdMonitor = false;
            }*/

            monitor.Naam = vm.Naam;
            monitor.Voornaam = vm.Voornaam;
            monitor.Email = vm.Email;
            monitor.IsActief = vm.IsActief;
            monitor.IsHoofdMonitor = vm.IsHoofdMonitor;


            _context.CustomUserRepository.Update(monitor);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public async Task<IActionResult>Details(string id, MonitorBeheerDetailViewModel vm)
        {
            var monitor = await _userManager.FindByIdAsync(id.ToString());
            if (monitor == null) { return NotFound(); }

            var monitorReizen =  await GetMonitorReizen(id);
            var opleidingen = await GetOpleidingen(id);

            vm = new MonitorBeheerDetailViewModel()
            {
                Naam = monitor.Naam,
                Voornaam = monitor.Voornaam,
                MonitorReizen = monitorReizen,
                Opleidingen = opleidingen
            };

            return View(vm);

        }

        public async Task<IEnumerable<Models.Monitor>> GetMonitorReizen(string id)
        {
            Expression<Func<Models.Monitor, bool>> filter = m => m.PersoonId == id;

            var monitorReizen = await _context.MonitorRepository.GetWithIncludesAsync(
            filter,
            m => m.Groepsreis,
            m => m.CustomUser,
            m => m.Groepsreis.Bestemming,
            m => m.Groepsreis.Bestemming.Fotos
            );

              return monitorReizen;
        }

        public async Task<IEnumerable<OpleidingPersoon>> GetOpleidingen(string userId)
        {
            var vandaag = DateTime.Now;

            Expression<Func<OpleidingPersoon, bool>> filter = op => op.PersoonId == userId;
            var opleidingen = await _context.OpleidingPersoonRepository.GetWithIncludesAsync(
                filter,
                op => op.CustomUser,
                op => op.Opleiding

                );

            return opleidingen;
        }
    }
}
