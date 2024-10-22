using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

namespace WebApp.Controllers;

public class ContactController : Controller
{

    private static Dictionary<int, ContactModel> _contacts = new()
    {
        {
            1, new ContactModel()
            {
                Id = 1,
                FirstName = "Adam",
                LastName = "Abecki",
                Email = "adam@wsei.edu.pl",
                BirthDate = new DateOnly(2000, 10, 10),
                PhoneNumber = "+48 222 222 222"
            }
        },
        {
            2, new ContactModel()
            {
                Id = 1,
                FirstName = "Ewa",
                LastName = "Bebecka",
                Email = "ewa@wsei.edu.pl",
                BirthDate = new DateOnly(2001, 10, 10),
                PhoneNumber = "+48 222 222 333"
            }
        },
        {
            3, new ContactModel()
            {
                Id = 3,
                FirstName = "Boba",
                LastName = "Bobov",
                Email = "bob@wsei.edu.pl",
                BirthDate = new DateOnly(1992, 10, 10),
                PhoneNumber = "+48 222 222 444"
            }
        },
    };


    private static int currentId = 3;
    // LISTA kontaktow 
    public IActionResult Index()
    {
        return View(_contacts);
    }
    // Formularz dodania kontaktu
    [HttpGet]
    public IActionResult Add()
    {
        return View();
    }
    [HttpPost]
    // Odebranie i zapisanie nowego kontaktu
    public IActionResult Add(ContactModel model)
    {
        if (!ModelState.IsValid) {
            return View(model);
        }

        model.Id = ++currentId;
        _contacts.Add(model.Id, model);
        return View("Index", _contacts);
    }

    public IActionResult Delete(int id)
    {
        _contacts.Remove(id);
        return View("Index", _contacts);
    }
}