using WebApp.Entities;
using WebApp.Models;

namespace WebApp;

public static class ContactMapper
{
    public static ContactEntity ToEntity(Contact contact)
    {
        return new ContactEntity
        {
            Id = contact.Id,
            Name = contact.Name,
            Email = contact.Email,
            Phone = contact.Phone,
            Birth = contact.Birth,
            OrganizationId = contact.OrganizationId,
            Organization = contact.Organization != null ? new OrganizationEntity
            {
                Id = contact.Organization.Id,
                Title = contact.Organization.Title
            } : null
        };
    }

    public static Contact FromEntity(ContactEntity entity)
    {
        return new Contact
        {
            Id = entity.Id,
            Name = entity.Name,
            Email = entity.Email,
            Phone = entity.Phone,
            Birth = entity.Birth,
            OrganizationId = entity.OrganizationId,
            Organization = entity.Organization != null ? new OrganizationEntity
            {
                Id = entity.Organization.Id,
                Title = entity.Organization.Title
            } : null
        };
    }
}