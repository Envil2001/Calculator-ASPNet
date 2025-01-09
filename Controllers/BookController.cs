using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

namespace WebApp.Controllers;

 public class BookController : Controller
    {
        private static Dictionary<int, Book> _books = new Dictionary<int, Book>();

        public IActionResult Index()
        {
            return View(_books);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Book model)
        {
            if (ModelState.IsValid)
            {
                int id = _books.Keys.Count != 0 ? _books.Keys.Max() : 0;
                model.Id = id + 1;
                _books.Add(model.Id, model);

                return RedirectToAction("Index");
            }
            else
            {
                return View(model);
            }
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            if (_books.ContainsKey(id))
            {
                return View(_books[id]);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public IActionResult Edit(Book model)
        {
            if (ModelState.IsValid)
            {
                _books[model.Id] = model;
                return RedirectToAction("Index");
            }
            else
            {
                return View(model);
            }
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            if (_books.ContainsKey(id))
            {
                return View(_books[id]);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            if (_books.ContainsKey(id))
            {
                return View(_books[id]);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            if (_books.ContainsKey(id))
            {
                _books.Remove(id);
            }
            return RedirectToAction("Index");
        }
    }