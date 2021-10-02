namespace WebExample.Controllers;

public class HomeController : Controller
{
    private readonly IServiceProvider _services;


    public HomeController(IServiceProvider services)
    {
        _services = services;
    }


    public IActionResult Index()
    {
        var dict = _services.GetPaneUnits(9, out var userIds);

        ViewBag.Editors = _services.GetEditorsByUserIds(userIds);

        return View(dict);
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