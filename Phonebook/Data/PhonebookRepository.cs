using Microsoft.EntityFrameworkCore;
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
        List<Contact> allContacts = await _phonebookContext.Contacts.ToListAsync<Contact>();
        return allContacts;
    }

    public async Task<Contact?> ReadContactDetails(int contactId)
    {
        Contact? contact = await _phonebookContext.Contacts.SingleOrDefaultAsync(c => c.Id == contactId);
        return contact;
    }

    internal async Task CreateNewContactAsync(Contact contact)
    {
        await _phonebookContext.Contacts.AddAsync(contact);
        await _phonebookContext.SaveChangesAsync();
    }
}
