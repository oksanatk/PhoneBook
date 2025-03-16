using System.Text.RegularExpressions;

namespace PhoneBook.Models;

class Contact
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public int Phone { get; set; }
    public string Email { get; set; } = null!;
    public ContactGroup Group { get; set; }
}
