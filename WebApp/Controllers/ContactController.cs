using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApp.Models;
using WebApp.Services;

namespace WebApp.Controllers;

public class ContactController : Controller
{
    private readonly IContactService _contactService;

    public ContactController(IContactService contactService)
    {
        _contactService = contactService;
    }

    public IActionResult Index()
    {
        var contacts = _contactService.FindAll(); 
        return View(contacts);
    }

    public IActionResult Create()
    {
        var model = new Contact
        {
            Organizations = _contactService.FindAllOrganizations()
                .Select(o => new SelectListItem { Value = o.Id.ToString(), Text = o.Title })
                .ToList()
        };
        return View(model);
    }


    [HttpPost]
    public IActionResult Create(Contact contact)
    {
        if (ModelState.IsValid)
        {
            _contactService.Add(contact);
            return RedirectToAction(nameof(Index));
        }

        contact.Organizations = _contactService.FindAllOrganizations()
            .Select(o => new SelectListItem { Value = o.Id.ToString(), Text = o.Title })
            .ToList();
        return View(contact);
    }
    public IActionResult Edit(int id)
    {
        var contact = _contactService.FindById(id);
        if (contact == null)
        {
            return NotFound();
        }
        return View(contact);
    }

    [HttpPost]
    public IActionResult Edit(Contact contact)
    {
        if (ModelState.IsValid)
        {
            _contactService.Update(contact);
            return RedirectToAction(nameof(Index));
        }
        return View(contact);
    }

    public IActionResult Delete(int id)
    {
        var contact = _contactService.FindById(id);
        if (contact == null)
        {
            return NotFound();
        }
        return View(contact);
    }

    [HttpPost, ActionName("Delete")]
    public IActionResult DeleteConfirmed(int id)
    {
        _contactService.Delete(id);
        return RedirectToAction(nameof(Index));
    }
}