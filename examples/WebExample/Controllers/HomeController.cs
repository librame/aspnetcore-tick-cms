namespace WebExample.Controllers;

public class HomeController : Controller
{
    private readonly IStore<Unit> _unitStore;

    public HomeController(IStore<Unit> unitStore)
    {
        _unitStore = unitStore;
    }

    public IActionResult Index()
    {
        return View(_unitStore.FindList());
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}