using Microsoft.AspNetCore.Mvc;
using Project_ZF.ViewModels;

namespace Project_ZF.Controllers
{
    public class LevelController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;


        public LevelController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }
        public async Task<IActionResult> Index()
        {
            try
            {
                var levels = _unitOfWork.LevelRepository.GetAll();

                var vm = new LevelViewModel
                {
                    Levels = levels.ToList()
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
            return View("Create");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LevelCreateViewModel vm)
        {
            if (ModelState.IsValid)
            {

                var level = new Level
                {
                    Naam = vm.Naam,
                    BenodigdePunten = vm.BenodigdePunten,
                   
                };


                await _unitOfWork.LevelRepository.AddAsync(level);
                await _unitOfWork.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            else
            {
               
                return View(vm);
            }
        }

        public async Task<IActionResult> Edit(int id)
        {

            var level = await _unitOfWork.LevelRepository.GetByIdAsync(id);
            if (level == null) { return NotFound(); }


            LevelAanpassenViewModel vm = new LevelAanpassenViewModel()
            {
                Id = level.Id,
                Naam = level.Naam,
                BenodigdePunten = level.BenodigdePunten
            };

            return View(vm);

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, LevelAanpassenViewModel vm)

        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            try
            {
                var level = await _unitOfWork.LevelRepository.GetByIdAsync(id);
                if (level == null)
                {
                    return NotFound();
                }

                level.Naam = vm.Naam;
                level.BenodigdePunten = vm.BenodigdePunten;
               


                _unitOfWork.LevelRepository.Update(level);
                await _unitOfWork.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                var errorViewModel = new ErrorViewModel { Message = $"Fout bij het bijwerken van level: {ex.Message}" };
                return View("Error", errorViewModel);
            }
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var level = await _unitOfWork.LevelRepository.GetByIdAsync(id);

            try
            {
                if (level != null)
                {
                    _unitOfWork.LevelRepository.Delete(level);
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
    }


}
