using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApp.Middleware;
using WebApp.Models;
using WebApp.Models.Movies;

namespace WebApp.Controllers
{
    public class MovieController : Controller
    {
        private readonly MoviesContext _context;

        public MovieController(MoviesContext context)
        {
            _context = context;
        }

        // GET: Movie
        public IActionResult Index(int page = 1, int size = 10)
        {
            ViewData["Visit"] = HttpContext.Items[LastVisitCookie.CookieName];
            
            var totalItems = _context.Movies.Count();
            var movies = _context.Movies
                .OrderBy(m => m.Title)
                .Skip((page - 1) * size)
                .Take(size)
                .AsAsyncEnumerable();

            var pagingList = PagingListAsync<Movie>.Create(
                (p, s) => movies,
                totalItems,
                page,
                size
            );

            return View(pagingList);
        }
        // GET: Movie/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movies
                .FirstOrDefaultAsync(m => m.MovieId == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // GET: Movie/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Movie/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MovieId,Title,Budget,Homepage,Overview,Popularity,ReleaseDate,Revenue,Runtime,MovieStatus,Tagline,VoteAverage,VoteCount")] Movie movie)
        {
            if (ModelState.IsValid)
            {
                _context.Add(movie);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(movie);
        }

        // GET: Movie/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movies.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }
            return View(movie);
        }

        // POST: Movie/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MovieId,Title,Budget,Homepage,Overview,Popularity,ReleaseDate,Revenue,Runtime,MovieStatus,Tagline,VoteAverage,VoteCount")] Movie movie)
        {
            if (id != movie.MovieId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(movie);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovieExists(movie.MovieId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(movie);
        }

        // GET: Movie/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movies
                .FirstOrDefaultAsync(m => m.MovieId == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // POST: Movie/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            // Удаляем связанные записи с помощью SQL-запросов
            await _context.Database.ExecuteSqlRawAsync("DELETE FROM movie_company WHERE movie_id = {0}", id);
            await _context.Database.ExecuteSqlRawAsync("DELETE FROM movie_cast WHERE movie_id = {0}", id);
            await _context.Database.ExecuteSqlRawAsync("DELETE FROM movie_genres WHERE movie_id = {0}", id);
            await _context.Database.ExecuteSqlRawAsync("DELETE FROM movie_keywords WHERE movie_id = {0}", id);
            await _context.Database.ExecuteSqlRawAsync("DELETE FROM movie_languages WHERE movie_id = {0}", id);
            await _context.Database.ExecuteSqlRawAsync("DELETE FROM movie_crew WHERE movie_id = {0}", id);
            await _context.Database.ExecuteSqlRawAsync("DELETE FROM production_country WHERE movie_id = {0}", id);

            // Удаляем сам фильм
            var movie = await _context.Movies.FindAsync(id);
            if (movie != null)
            {
                _context.Movies.Remove(movie);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
        private bool MovieExists(int id)
        {
            return _context.Movies.Any(e => e.MovieId == id);
        }
        
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
