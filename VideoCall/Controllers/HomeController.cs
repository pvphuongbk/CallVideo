using Microsoft.AspNetCore.Mvc;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult VideoCall()
    {
        return View();
    }
    public IActionResult VoiceCall()
    {
        return View();
    }
}
