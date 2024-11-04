using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Models;

public class ContactModel
{
    [HiddenInput]
    public int Id { get; set; }
    
    [Required(ErrorMessage = "Musisz podać imię!")]
    [MaxLength(length: 20, ErrorMessage = "Imie nie może być dłuższe niż 20 znaków!")]
    [MinLength(length: 2)]
    [Display(Name = "Imie")]
    public string FirstName { get; set; }
    
    [Required(ErrorMessage = "Musisz podać nazwiski!")]
    [MinLength(length: 2)]
    [MaxLength(length: 50, ErrorMessage = "Nazwisko nie może być dłuższe niż 50 znaków!")]
    [Display(Name = "Nazwisko")]
    public string LastName { get; set; }
    
    [EmailAddress]
    [Display(Name = "Adres E-mail")]
    public string Email { get; set; }
    
    
    [Phone]
    [Display(Name = "Telefon")]
    public string PhoneNumber { get; set; }
    
    [Display(Name = "Data urodzenia")]
    [DataType(DataType.Date)]
    public DateOnly BirthDate { get; set; }
    
    [Display(Name = "Kategoria")]
    
    public Category Category { get; set; }

}