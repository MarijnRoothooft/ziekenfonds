using Microsoft.AspNetCore.Mvc;
using Project_ZF.ViewModels;
using System.Linq.Expressions;

namespace Project_ZF.Controllers
{
    public class PuntenController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private UserManager<CustomUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;
        private SignInManager<CustomUser> _signInManager;

       
        public PuntenController(ILogger<HomeController> logger, UserManager<CustomUser> userManager, RoleManager<IdentityRole> roleManager, SignInManager<CustomUser> signInManager, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index(string id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                return NotFound();
            }

            var reizen = await GetReizen(id);
            var levelBeloning = await GetUserLevelBeloning(id);

            PuntenViewModel vm = new PuntenViewModel()
            {
                AantalPunten = user.AantalPunten,
                Groepsreizen = reizen,
                Voornaam = user.Voornaam,
                Achternaam = user.Naam,
                LevelBeloning = levelBeloning

            };


            return View(vm);
        }

        public async Task<IEnumerable<Punten>> GetReizen(string id)
        {
            Expression<Func<Punten, bool>> filter = p => p.Deelnemer.Kind.CustomUser.Id == id;
            var reizen = await _unitOfWork.PuntenRepository.GetWithIncludesAsync(
                   filter,
                   p => p.Groepsreis,
                   p => p.Groepsreis.Bestemming,
                   p => p.Groepsreis.Bestemming.Fotos,
                   p => p.Deelnemer.Kind
                   );
            return reizen;

        }

        public async Task<IEnumerable<CustomUser>> GetUserLevelBeloning(string id)
        {
            Expression<Func<CustomUser, bool>> filter = p => p.Id == id;
            var levelBeloning = await _unitOfWork.CustomUserRepository.GetWithIncludesAsync(
                filter,
                p => p.Level,
                p => p.Level.Beloningen
                );

            return levelBeloning;

        }

    
    }
}
