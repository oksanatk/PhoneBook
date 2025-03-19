using System.Text.RegularExpressions;

namespace PhoneBook.Utilities;

public static class Validator
{
    public static bool ValidateEmail(string maybeEmail)
    {
        maybeEmail = maybeEmail.Trim().ToLower();

        if (Regex.IsMatch(maybeEmail, @"^[a-z0-9]+[a-z0-9.]*@[a-z0-9]+[a-z0-9.]*\.[a-z0-9]{2,}$"))
        {
            return true;
        }
        return false;
    }

    public static bool ValidatePhone(string maybePhone)
    {

        if (!string.IsNullOrWhiteSpace(maybePhone) && Regex.IsMatch(maybePhone, @"^(\+\d{1,2}\s?)?1?\-?\.?\s?\(?\d{3}\)?[\s.-]?\d{3}[\s.-]?\d{4}$")) ;
        {
            return true;
        } 
        return false;
    }
}
