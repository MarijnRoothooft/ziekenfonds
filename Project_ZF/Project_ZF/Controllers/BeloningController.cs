using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project_ZF.ViewModels;
using System.Linq.Expressions;

namespace Project_ZF.Controllers
{
    public class BeloningController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        

        public BeloningController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
          
        }
        public async Task <IActionResult> Index()
        {
            try
            {
                var beloningen = await GetBeloningen();

                var vm = new BeloningViewModel
                {
                    Beloningen = beloningen
                };

                return View(vm);
               
            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel { Message = ex.Message });
            }
        }

        public async Task<IActionResult> Create()
        {
            
            var levels = await _unitOfWork.LevelRepository.GetAllAsync();

            var viewModel = new BeloningToevoegenViewModel
            {
                Levels = levels.ToList()
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BeloningToevoegenViewModel vm)
        {
            if (ModelState.IsValid)
            {

                var beloning = new Beloning
                {
                    Naam = vm.Naam,
                    Beschrijving = vm.Beschrijving,
                    LevelId = vm.LevelId
                };


                await _unitOfWork.BeloningRepository.AddAsync(beloning);
                await _unitOfWork.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            else
            {
                var levels = await _unitOfWork.LevelRepository.GetAllAsync();
                vm.Levels = levels.ToList();
                return View(vm);
            }
        }

        public async Task<IActionResult> Edit(int id)
        {
           
            var beloning = await _unitOfWork.BeloningRepository.GetByIdAsync(id);
            if (beloning == null) { return NotFound(); }

            var levels = await _unitOfWork.LevelRepository.GetAllAsync();


            BeloningWijzigenViewModel vm = new BeloningWijzigenViewModel()
            {
                Id = beloning.Id,
                Naam = beloning.Naam,
                Beschrijving = beloning.Beschrijving,
                LevelId = beloning.LevelId,
                Levels = levels.ToList()
            };

            return View(vm);

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, BeloningWijzigenViewModel vm)

        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            try
            {
                var beloning = await _unitOfWork.BeloningRepository.GetByIdAsync(id);
                if (beloning == null)
                {
                    return NotFound();
                }

                beloning.Naam = vm.Naam;
                beloning.Beschrijving = vm.Beschrijving;
                beloning.LevelId = vm.LevelId;
               

                _unitOfWork.BeloningRepository.Update(beloning);
                await _unitOfWork.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                var errorViewModel = new ErrorViewModel { Message = $"Fout bij het bijwerken van de beloning: {ex.Message}" };
                return View("Error", errorViewModel);
            }
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var beloning = await _unitOfWork.BeloningRepository.GetByIdAsync(id);
            

            try
            {
                if (beloning != null)
                {
                    _unitOfWork.BeloningRepository.Delete(beloning);
                    await _unitOfWork.SaveChangesAsync();
                }
                else
                {
                    throw new Exception($"{id} niet gevonden");
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return View("Error");
            }
        }


        public async Task<IEnumerable<Beloning>> GetBeloningen()
        {

            Expression<Func<Beloning, bool>> filter = g => true;
                var beloningen = await _unitOfWork.BeloningRepository.GetWithIncludesAsync(
                    filter,
                   g => g.Level
                    );

                return beloningen;

            }

        }

        
    }

