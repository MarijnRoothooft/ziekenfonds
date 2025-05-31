using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Project_ZF.Data;
using Project_ZF.Data.Repository.UnitOfWork;
using Project_ZF.Models;
using Project_ZF.ViewModels;

namespace Project_ZF.Controllers
{
    public class MonitorController : Controller
    {

       // private readonly IUnitOfWork _context;
        private UserManager<CustomUser> _userManager;
        private SignInManager<CustomUser> _signInManager;
        private RoleManager<IdentityRole> _roleManager;
        private readonly IUnitOfWork _unitOfWork;



        public MonitorController( UserManager<CustomUser> userManager, RoleManager<IdentityRole> roleManager, SignInManager<CustomUser> signInManager, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _unitOfWork = unitOfWork;


        }


        [Authorize(Roles = "monitor,hoofdmonitor")]



        public async Task<IActionResult> Index(int id)
        {
           

            var monitoren = _unitOfWork.MonitorRepository.GetAllIncluding(b => b.CustomUser)
                .Where(b => b.GroepsreisDetailsId == id)
                .Select(b => new MonitorViewModel
                {  
                PersoonId = b.PersoonId,
                CustomUser = b.CustomUser,  
                }).ToList();

            // Ophalen van de gebruiker gegevens
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return RedirectToPage("/Account/Login", new { area = "Identity" });
            }

            foreach (var item in monitoren)
            {
                if (currentUser == item.CustomUser)
                    return View(monitoren);

            }
            return View(null);

        }

       
    }
}
  