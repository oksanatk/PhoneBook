using PhoneBook.Models;

namespace PhoneBook.Utilities;

class ContactGroupExtension
{
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
