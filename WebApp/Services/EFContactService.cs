using Microsoft.EntityFrameworkCore;
using WebApp.Entities;
using WebApp.Models;

namespace WebApp.Services;

public class EFContactService : IContactService
{
    private AppDbContext _context;

    public EFContactService(AppDbContext context)
    {
        _context = context;
    }

    public List<OrganizationEntity> FindAllOrganizations()
    {
        return _context.Organizations.ToList();
    }

    public int Add(Contact contact)
    {
        var e = _context.Contacts.Add(ContactMapper.ToEntity(contact));
        _context.SaveChanges();
        return e.Entity.Id;
    }

    public void Delete(int id)
    {
        ContactEntity? find = _context.Contacts.Find(id);
        if (find != null)
        {
            _context.Contacts.Remove(find);
            _context.SaveChanges();
        }
    }

    public List<Contact> FindAll()
    {
        return _context.Contacts
            .Include(c => c.Organization) 
            .Select(e => ContactMapper.FromEntity(e))
            .ToList();
    }

    public Contact? FindById(int id)
    {
        var entity = _context.Contacts.Find(id);
        return entity != null ? ContactMapper.FromEntity(entity) : null;
    }

    public void Update(Contact contact)
    {
        var entity = ContactMapper.ToEntity(contact);
        _context.Contacts.Update(entity);
        _context.SaveChanges();
    }
}