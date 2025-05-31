using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Project_ZF.ViewModels;
using System.Linq.Expressions;

namespace Project_ZF.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<CustomUser> _userManager;

        public DashboardController(IUnitOfWork unitOfWork, UserManager<CustomUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        [Authorize(Roles = "admin, user, monitor, hoofdmonitor, verantwoordelijke, beheerder, gebruiker")]
        public async Task<IActionResult> Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToPage("/Account/Login", new { area = "Identity" });
            }

            var userId = _userManager.GetUserId(User);

            if (userId == null)
            {
                return RedirectToPage("/Account/Login", new { area = "Identity" });
            }

            
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound("User not found");
            }

            var reizen = await GetReizen(user.Id);
            var groepsReizen = await GetAllReizen();
            var monitorReizen = await GetMonitorReizen(user.Id);
            var opleidingen = await GetOpleidingen (user.Id);
            var kinderen = await GetKinderen(user.Id);
            var beheerdersGroepreizen = await GetBeheerderReizen();

            var viewModel = new DashboardViewModel
            {
                User = user,
                Reizen = reizen,
                Groepsreizen = groepsReizen,
                MonitorReizen = monitorReizen,
                Opleidingen = opleidingen,
                Kinderen = kinderen,
                BeheerdersGroepreizen = beheerdersGroepreizen
            };


            return View(viewModel);
        }

        [Authorize(Roles = "admin, user, monitor, hoofdmonitor, verantwoordelijke, beheerder, gebruiker")]
        public async Task<IEnumerable<Deelnemer>> GetReizen(string userId)
        {
           
            var vandaag = DateTime.Now;
            
                Expression<Func<Deelnemer, bool>> filter = d => d.Kind.PersoonId == userId && d.Groepsreis.EindDatum.AddDays(7) >= vandaag;
                var reizen = await _unitOfWork.DeelnemerRepository.GetWithIncludesAsync(
                    filter,
                    d => d.Kind,
                    d => d.Kind.CustomUser,
                    d => d.Groepsreis,
                    d => d.Groepsreis.Bestemming,
                    d => d.Groepsreis.Bestemming.Fotos
                    );

                return reizen;
            
        }

        [Authorize(Roles = "gebruiker")]
        public async Task <IEnumerable<Groepsreis>> GetAllReizen()
        {
            var vandaag = DateTime.Now;

            Expression<Func<Groepsreis, bool>> filter = g => true && g.BeginDatum > vandaag;
            var groepsReizen = await _unitOfWork.GroepsreisRepository.GetWithIncludesAsync(
                filter,
                g => g.Bestemming,
                g => g.Bestemming.Fotos

                );

            return groepsReizen;

        }

        [Authorize(Roles = "monitor, hoofdmonitor")]
        public async Task<IEnumerable<Models.Monitor>> GetMonitorReizen (string userId)
        {
            var vandaag = DateTime.Now;

            Expression<Func<Models.Monitor, bool>> filter = m => m.PersoonId == userId && m.Groepsreis.EindDatum.AddDays(7) >= vandaag;
            var monitorReizen = await _unitOfWork.MonitorRepository.GetWithIncludesAsync(
                filter,
                m => m.Groepsreis,
                m => m.CustomUser,
                m => m.Groepsreis.Bestemming,
                m => m.Groepsreis.Bestemming.Fotos
                );

            return monitorReizen;
        }

        [Authorize(Roles = "monitor, hoofdmonitor")]
        public async Task<IEnumerable<OpleidingPersoon>> GetOpleidingen (string userId)
        {
            var vandaag = DateTime.Now;

            Expression<Func<OpleidingPersoon, bool>> filter = op => op.PersoonId == userId && op.Opleiding.EindDatum.AddDays(7) >= vandaag;
            var opleidingen = await _unitOfWork.OpleidingPersoonRepository.GetWithIncludesAsync(
                filter,
                op => op.CustomUser,
                op => op.Opleiding

                );

            return opleidingen;
        } 

        public async Task<IEnumerable<Kind>> GetKinderen (string userId)
        {
            Expression<Func<Kind, bool>> filter = k => k.PersoonId == userId;
            var kinderen = await _unitOfWork.KindRepository.GetWithIncludesAsync(
                filter,
                k => k.CustomUser);

            return kinderen;
        }

        [Authorize(Roles = "beheerder, verantwoordelijke")]
        public async Task<IEnumerable<Groepsreis>> GetBeheerderReizen()
        {
            var vandaag = DateTime.Now;

            Expression<Func<Groepsreis, bool>> filter = r => r.BeginDatum >= vandaag;
            var beheerdersReizen = await _unitOfWork.GroepsreisRepository.GetWithIncludesAsync(
                filter,
                r => r.Monitoren,
                r => r.Bestemming,
                r => r.Bestemming.Fotos,
                r => r.Deelnemers
                );
            
            return beheerdersReizen;
        }
    }
}
