using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using VypocetStatiky.Models;

namespace VypocetStatiky.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        InputClass input = new InputClass();
        return View(input);
    }

    [HttpPost]
    public IActionResult Index(InputClass model)
    {
        if (ModelState.IsValid)
        {
            model.DejCislaDoSeznamu();
            model.Secti();
            model.NejmensiCislo();
            model.NejvetsiCislo();
            model.Prumer();
            return View(model);
        }
        return View(model);
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