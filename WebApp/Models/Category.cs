using System.ComponentModel.DataAnnotations;

namespace WebApp.Models;

public enum Category
{
    [Display(Name = "Rodzina", Order = 1)]
    Family = 1,
    [Display(Name = "Znajomi", Order = 2)]
    Friend = 2,
    [Display(Name = "Kontakt zawodowy", Order = 3)]
    Business = 3,
}