using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;

public class CalculatorController : Controller
{
    public IActionResult Form()
    {
        return View();
    }

    [HttpGet]
    public IActionResult Result(double a, double b, string op)
    {
        var model = new Calculator
        {
            X = a,
            Y = b,
            Operator = op switch
            {
                "add" => Operators.Add,
                "sub" => Operators.Sub,
                "mul" => Operators.Mul,
                "div" => Operators.Div,
                _ => null
            }
        };

        if (!model.IsValid())
        {
            ViewBag.ErrorMessage = "Niepoprawne dane. Upewnij się, że wprowadziłeś poprawne liczby i operator.";
            return View("Form");
        }

        ViewBag.Result = model.Calculate();
        return View(model);
    }

    [HttpPost]
    public IActionResult Result([FromForm] Calculator model)
    {
        if (!model.IsValid())
        {
            ViewBag.ErrorMessage = "Niepoprawne dane. Upewnij się, że wprowadziłeś poprawne liczby i operator.";
            return View("Form");
        }

        ViewBag.Result = model.Calculate();
        return View(model);
    }
}