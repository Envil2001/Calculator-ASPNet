using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

namespace WebApp.Controllers;

public enum Operator
{
    Add, Sub, Mul, Div
}

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }
    // Dodaj plik widoku 
    public IActionResult Calculator(Operator? op, double? a, double? b)
    {
        if (a is null || b is null)
        {
            ViewBag.ErrorMessage = "Niepoprawny format liczby w parametrze a lub b";
            return View("CustomError");
        }

        if (op is null)
        {
            ViewBag.Result = "Invalid operation";
            return View("CustomError");
        }
        ViewBag.Num1 = a;
        ViewBag.Num2 = b;
        

        switch (op)
        {
            case Operator.Add:
                ViewBag.Result = a + b;
                ViewBag.Operator = "+";
                break;
            case Operator.Sub:
                ViewBag.Result = a - b;
                ViewBag.Operator = "-";
                break;
            case Operator.Div:
                ViewBag.Result = a * b;
                ViewBag.Operator = "*";
                break;
            case Operator.Mul:
                if (b != 0)
                {
                    ViewBag.Result = a / b;
                }
                else
                {
                    ViewBag.Result = "Cannot divide by zero";
                    return View();
                }
                ViewBag.Operator = "/";
                break;
        }
        
        return View();
    }

    public IActionResult About()
    {
        return View();
    }
    
    public IActionResult Index()
    {
        return View();
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

