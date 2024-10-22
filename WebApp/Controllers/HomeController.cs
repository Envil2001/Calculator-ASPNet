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
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["User"] = "Bomj"; 
            return View();
        }

        public IActionResult Calculator(string op, double? a, double? b)
        {
            if (op == null || a == null || b == null)
            {
                ViewBag.ErrorMessage = "Nie wszystkie parametry zostały podane. Upewnij się, że przekazano op, a oraz b.";
                return View("CustomError");
            }

            double result = 0;
            string operatorSymbol;

            switch (op.ToLower())
            {
                case "add":
                    result = a.Value + b.Value;
                    operatorSymbol = "+";
                    break;
                case "sub":
                    result = a.Value - b.Value;
                    operatorSymbol = "-";
                    break;
                case "mul":
                    result = a.Value * b.Value;
                    operatorSymbol = "*";
                    break;
                case "div":
                    if (b.Value == 0)
                    {
                        ViewBag.ErrorMessage = "Błąd: dzielenie przez zero jest niemożliwe.";
                        return View("CustomError");
                    }
                    result = a.Value / b.Value;
                    operatorSymbol = "/";
                    break;
                default:
                    ViewBag.ErrorMessage = "Nieprawidłowy operator.";
                    return View("CustomError");
            }

            ViewBag.Num1 = a;
            ViewBag.Num2 = b;
            ViewBag.Operator = operatorSymbol;
            ViewBag.Result = result;

            return View();
        }
    }