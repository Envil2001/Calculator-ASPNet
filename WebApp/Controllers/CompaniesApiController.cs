using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp.Models.Movies;

namespace WebApp.Controllers;

[Route("api/companies")]
[ApiController]
public class CompaniesApiController : ControllerBase
{
    private readonly MoviesContext _context;

    public CompaniesApiController(MoviesContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult GetFiltered(string filter)
    {
        var companies = _context.ProductionCompanies
            .Where(c => c.CompanyName.ToLower().Contains(filter.ToLower()))
            .OrderBy(c => c.CompanyName)
            .AsNoTracking()
            .ToList();

        return Ok(companies);
    }
}