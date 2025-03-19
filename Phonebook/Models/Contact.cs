using System.Text.RegularExpressions;

namespace PhoneBook.Models;

class Contact
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Phone { get; set; } = null!;
    public string Email { get; set; } = null!;
    public ContactGroup Group { get; set; }

    internal static ContactGroup GetGroupFromString(string contactGroup)
    {
        switch (contactGroup)
        {
            case "Work":
                return ContactGroup.Work;
            case "Friend":
                return ContactGroup.Friend;
            case "Hobby":
                return ContactGroup.Hobby;
            case "Volunteer":
                return ContactGroup.Volunteer;
            case "Sport":
                return ContactGroup.Sport;
            case "Faith":
                return ContactGroup.Faith;
            default:
                return ContactGroup.Work;
        }
    }
}
