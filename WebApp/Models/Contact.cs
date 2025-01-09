using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Models;

public class Contact
{
    [HiddenInput]
    public int Id { get; set; }

    [Required(ErrorMessage = "Proszę podać imię!")]
    [Display(Name = "Imię")]
    public string Name { get; set; }

    [EmailAddress(ErrorMessage = "Proszę podać poprawny adres email!")]
    [Display(Name = "Email")]
    public string Email { get; set; }

    [Display(Name = "Priorytet")]
    public Priority Priority { get; set; }

    [HiddenInput]
    [Display(Name = "Data utworzenia")]
    public DateTime Created { get; set; }
}