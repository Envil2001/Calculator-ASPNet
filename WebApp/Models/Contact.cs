using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApp.Entities;

namespace WebApp.Models;

public class Contact
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public DateTime Birth { get; set; }

    [HiddenInput]
    public int OrganizationId { get; set; }

    [ValidateNever]
    public OrganizationEntity? Organization { get; set; }

    [ValidateNever]
    public List<SelectListItem> Organizations { get; set; }
}