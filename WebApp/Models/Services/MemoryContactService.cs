namespace WebApp.Models.Services;

public class MemoryContactService : IContactService
{
    private Dictionary<int, ContactModel> _contacts = new()
    {
        {
            1, new ContactModel()
            {
                Id = 1,
                Category = Category.Business,
                FirstName = "Adam",
                LastName = "Abecki",
                Email = "adam@wsei.edu.pl",
                BirthDate = new DateOnly(2000, 10, 10),
                PhoneNumber = "+48 222 222 222"
            }
        },
        {
            2, new ContactModel()
            {
                Id = 1,
                Category = Category.Family,
                FirstName = "Ewa",
                LastName = "Bebecka",
                Email = "ewa@wsei.edu.pl",
                BirthDate = new DateOnly(2001, 10, 10),
                PhoneNumber = "+48 222 222 333"
            }
        },
        {
            3, new ContactModel()
            {
                Id = 3,
                Category = Category.Family,
                FirstName = "Boba",
                LastName = "Bobov",
                Email = "bob@wsei.edu.pl",
                BirthDate = new DateOnly(1992, 10, 10),
                PhoneNumber = "+48 222 222 444"
            }
        },
    };

    private int _index = 3;

    public void Add(ContactModel model)
    {
        model.Id = ++_index;
        _contacts.Add(model.Id, model);
    }

    public void Update(ContactModel model)
    {
        if (_contacts.ContainsKey(model.Id))
        {
            _contacts[model.Id] = model;
        }
    }

    public void Delete(int id)
    {
        _contacts.Remove(id);
    }

    public List<ContactModel> GetAll()
    {
        return _contacts.Values.ToList();
    }

    public ContactModel? GetById(int id)
    {
        return _contacts[id];
    }
}

