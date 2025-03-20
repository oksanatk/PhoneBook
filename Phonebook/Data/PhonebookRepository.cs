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

    internal async Task CreateNewContactAsync(Contact contact)
    {
        await _phonebookContext.Contacts.AddAsync(contact);
        await _phonebookContext.SaveChangesAsync();
    }

    internal async Task UpdateContact(Contact newContactValues)
    {
        Contact? oldContact = await _phonebookContext.Contacts.SingleOrDefaultAsync(c => c.Id == newContactValues.Id);
        if (oldContact != null)
        {
            _phonebookContext.Entry(oldContact).CurrentValues.SetValues(newContactValues);
            await _phonebookContext.SaveChangesAsync();
        }
    }

    internal async Task DeleteContactAsync(int id)
    {
        Contact? contactFound = _phonebookContext.Contacts.SingleOrDefault(c => c.Id == id);
        if (contactFound != null)
        {
            _phonebookContext.Remove(contactFound);
            await _phonebookContext.SaveChangesAsync();
        }
    }
}
