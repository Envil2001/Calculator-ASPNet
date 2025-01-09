using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Models;

public class Book
{
    [HiddenInput]
    public int Id { get; set; }

    [Required(ErrorMessage = "Proszę podać tytuł!")]
    public string Title { get; set; }

    [Required(ErrorMessage = "Proszę podać autora!")]
    public string Author { get; set; }

    [Range(1, 10000, ErrorMessage = "Liczba stron musi być między 1 a 10000!")]
    public int PageCount { get; set; }

    [Required(ErrorMessage = "Proszę podać ISBN!")]
    public string ISBN { get; set; }

    [Range(1900, 2100, ErrorMessage = "Rok wydania musi być między 1900 a 2100!")]
    public int YearPublished { get; set; }

    [Required(ErrorMessage = "Proszę podać wydawnictwo!")]
    public string Publisher { get; set; }
}