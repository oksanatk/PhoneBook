using System.Text.RegularExpressions;

namespace PhoneBook.Utilities;

public static class Formatter
{
    public static string FormatPhoneNumber(string validPhone)
    {
        validPhone = validPhone.Trim();
        validPhone = Regex.Replace(validPhone, @"[()\-_ .#+]", ""); // make plain 1234567890 number
        validPhone = Regex.Replace(validPhone, @"(\d{1,3})*(\d{3})(\d{3})(\d{4})", @"$1 ($2) $3-$4"); //format

        validPhone = validPhone.StartsWith(' ') ? ("+1" + validPhone) : ("+" + validPhone); //if no area code, assume +1
        return validPhone;
    }

    public static string SanitizeFormattedPhoneNumber(string contactPhone)
    {
        contactPhone = contactPhone.Trim();
        contactPhone = Regex.Replace(contactPhone, @"[()\s\-_#+]", "");
        contactPhone = Regex.Replace(contactPhone, @"(\d{1,3})*(\d{3})(\d{3})(\d{4})", @"$2$3$4");

        return contactPhone;
    }
}
