using Project_ZF.ViewModels;

public class ActiviteitController : Controller
{
    private readonly ILogger<ActiviteitController> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public ActiviteitController(IUnitOfWork unitOfWork, ILogger<ActiviteitController> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<IActionResult> Index(int groepsreisId)
    {
        var activiteiten = await _unitOfWork.ActiviteitRepository.GetAllAsync();
        var viewModel = activiteiten.Select(a => new ActiviteitViewModel
        {
            Id = a.Id,
            Naam = a.Naam,
            Beschrijving = a.Beschrijving
        }).ToList();

        ViewBag.GroepsreisId = groepsreisId;
        return View(viewModel);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(ActiviteitViewModel model)
    {
        if (ModelState.IsValid)
        {
            var activiteit = new Activiteit
            {
                Naam = model.Naam,
                Beschrijving = model.Beschrijving
            };

            await _unitOfWork.ActiviteitRepository.AddAsync(activiteit);
            _unitOfWork.SaveChanges();

            return RedirectToAction("Index");
        }

        return View(model);
    }

    //[HttpGet]
    //public IActionResult AddToGroepsreis(int groepsreisId)
    //{
    //    ViewBag.GroepsreisId = groepsreisId;
    //    var activiteiten = _unitOfWork.ActiviteitRepository.GetAll();
    //    return View(activiteiten);
    //}

    //[HttpPost]
    //public async Task<IActionResult> AddActiviteitToGroepsreis(int groepsreisId, int activiteitId)
    //{
    //    var groepsreis = await _unitOfWork.GroepsreisRepository.GetByIdAsync(groepsreisId);
    //    var activiteit = await _unitOfWork.ActiviteitRepository.GetByIdAsync(activiteitId);

    //    if (groepsreis != null && activiteit != null)
    //    {
    //        var programma = new Programma
    //        {
    //            GroepsreisId = groepsreisId,
    //            ActiviteitId = activiteitId,
    //        };

    //        await _unitOfWork.ProgrammaRepository.AddAsync(programma);
    //        await _unitOfWork.SaveChangesAsync();
    //    }

    //    return RedirectToAction("Index", "Bestemming");
    //}
}