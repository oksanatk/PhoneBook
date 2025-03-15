using PhoneBook.Models;

namespace PhoneBook.Data;

class PhonebookRepository
{
    private readonly PhonebookContext _phonebookContext;

    public PhonebookRepository(PhonebookContext context)
    {
        _phonebookContext = context;
    }

    public async Task<List<Contact>> ReadAllContactsAsync()
    {

        return new List<Contact>();
    }
}
