using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

namespace WebApp.Controllers;

public enum OperatorCalc
{
    Add, Sub, Mul, Div
}


public class CalculatorController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Form()
    {
        return View();
    }


    [HttpPost]
    public IActionResult Result(Calculator model)
    {
        if (!model.IsValid())
        {
            return View("Error");
        }
        return View(model);
    }

}