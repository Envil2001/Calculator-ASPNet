using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

namespace WebApp.Controllers;

public class BirthController : Controller
{
    public IActionResult Form()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Result([FromForm] Birth model)
    {
        if (!model.IsValid())
        {
            ViewBag.ErrorMessage = "Niepoprawne dane.";
            return View("Form");
        }

        ViewBag.Name = model.Name;
        ViewBag.Age = model.CalculateAge();
        return View();
    }
}