using WebApp.Models;
using WebApp.Models.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;

public class ContactController : Controller
{

    private readonly IContactService _contactService;

    public ContactController(IContactService contactService)
    {
        _contactService = contactService;
    }

    // GET: ContactController
    public ActionResult Index()
    {
        return View(_contactService.GetAll());
    }
    
    // GET: ContactController/Detail/5
    public ActionResult Details(int id)
    {
        return View(_contactService.GetById(id));
    }
    
    // GET: ContactController/Create
    [HttpGet]
    public ActionResult Create()
    {
        return View();
    }
    
    // POST: ContactController/Create
    [HttpPost]
    public ActionResult Create(ContactModel model)
    {
        if (!ModelState.IsValid) {
            return View(model);
        }

        _contactService.Add(model);
        return RedirectToAction(nameof(Index));
    }

    // GET: ContactController/Edit/5
    public ActionResult Edit(int id)
    {
        return View(_contactService.GetById(id));
    }
    
    //POST: ContactController/Edit/5
    [HttpPost]
    public ActionResult Edit(ContactModel model)
    {
        if (!ModelState.IsValid)
        {
            return View();
        }
        
        _contactService.Update(model);
        return RedirectToAction(nameof(Index));
    }
    
    // GET: ContactController/Delete/5
    public ActionResult Delete(int id)
    {
        return View();
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Delete(int id, ContactModel model)
    {
        _contactService.Delete(id);
        return RedirectToAction(nameof(Index));
    }
}