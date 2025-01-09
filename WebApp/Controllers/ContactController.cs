using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

public class ContactController : Controller
{
    private readonly IContactService _contactService;

    public ContactController(IContactService contactService)
    {
        _contactService = contactService;
    }

    public IActionResult Index()
    {
        return View(_contactService.FindAll());
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(Contact model)
    {
        if (ModelState.IsValid)
        {
            _contactService.Add(model);
            return RedirectToAction("Index");
        }
        return View(model);
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        var book = _contactService.FindById(id);
        if (book == null)
        {
            return NotFound();
        }
        return View(book);
    }

    [HttpPost]
    public IActionResult Edit(Contact model)
    {
        if (ModelState.IsValid)
        {
            _contactService.Update(model);
            return RedirectToAction("Index");
        }
        return View(model);
    }

    [HttpGet]
    public IActionResult Details(int id)
    {
        var book = _contactService.FindById(id);
        if (book == null)
        {
            return NotFound();
        }
        return View(book);
    }

    [HttpGet]
    public IActionResult Delete(int id)
    {
        var book = _contactService.FindById(id);
        if (book == null)
        {
            return NotFound();
        }
        return View(book);
    }

    [HttpPost]
    public IActionResult DeleteConfirmed(int id)
    {
        _contactService.Delete(id);
        return RedirectToAction("Index");
    }
}