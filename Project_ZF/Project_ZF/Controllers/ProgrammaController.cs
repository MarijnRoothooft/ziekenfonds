namespace Project_ZF.Controllers
{
    public class ProgrammaController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProgrammaController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        public async Task<IActionResult> AddActiviteitToGroepsreis(int groepsreisId, int activiteitId)
        {
            var groepsreis = await _unitOfWork.GroepsreisRepository.GetByIdAsync(groepsreisId);
            var activiteit = await _unitOfWork.ActiviteitRepository.GetByIdAsync(activiteitId);

            if (groepsreis != null && activiteit != null)
            {
                var programma = new Programma
                {
                    GroepsreisId = groepsreisId,
                    ActiviteitId = activiteitId,
                };

                await _unitOfWork.ProgrammaRepository.AddAsync(programma);
                await _unitOfWork.SaveChangesAsync();
            }

            return RedirectToAction("Index", "Bestemming");
        }
    }
}
