using PhoneBook.Data;
using PhoneBook.Models;
using Spectre.Console;
using PhoneBook.Utilities;
using PhoneBook.Services;

namespace PhoneBook.Views;

class PhonebookView
{
    private readonly PhonebookRepository _repository;
    private readonly EmailService _emailService;

    public PhonebookView(PhonebookRepository repository, EmailService emailService)
    {
        _repository = repository;
        _emailService = emailService;
    }

    public async Task ShowMainMenu()
    {
        string mainMenuChoice = "";
        do
        {
            Console.Clear();
            mainMenuChoice = AnsiConsole.Prompt(
                            new SelectionPrompt<string>()
                            .Title("[bold yellow]Welcome to the Phonebook App![/]")
                            .PageSize(10)
                            .AddChoices(new string[] {
                            "View Contacts",
                            "Create Contact",
                            "Edit Contact",
                            "Delete Contact",
                            "Send Email",
                            "Quit"})
                            .MoreChoicesText("[grey]Move up and down to view more options[/]"));

            switch (mainMenuChoice)
            {
                case "View Contacts":
                    await SelectFromAllContacts(MenuMode.View);
                    break;
                case "Create Contact":
                    await GetCreateContactInputs();
                    break;
                case "Edit Contact":
                    await SelectFromAllContacts(MenuMode.Update);
                    break;
                case "Delete Contact":
                    await SelectFromAllContacts(MenuMode.Delete);
                    break;
                case "Send Email":
                    await SelectFromAllContacts(MenuMode.SendEmail);
                    break;
                case "Quit":
                    break;
                default:
                    AnsiConsole.MarkupLine("[maroon]There seems to have been some type of issue. Press Enter to try again.[/]");
                    Console.ReadLine();
                    break;
            }
        } while (mainMenuChoice != "Quit");
    }

    public async Task SelectFromAllContacts(MenuMode menuMode)
    {
        Contact contactChoice;
        do
        {
            List<Contact> allContacts = await _repository.ReadAllContactsAsync();
            allContacts.Add(new Contact { Name = "Go Back" });

            Console.Clear();
            contactChoice = AnsiConsole.Prompt<Contact>(
                                    new SelectionPrompt<Contact>()
                                        .Title($"Choose the contact to {menuMode.ToString().ToLower()}")
                                        .AddChoices(allContacts)
                                        .UseConverter<Contact>(c => c.Name)
                                        );

            if (contactChoice.Name != "Go Back")
            {
                switch (menuMode)
                {
                    case MenuMode.Create:
                        await GetCreateContactInputs();
                        break;
                    case MenuMode.Update:
                        await GetUpdateContactInputs(contactChoice);
                        break;
                    case MenuMode.Delete:
                        await DeleteContact(contactChoice);
                        break;
                    case MenuMode.SendEmail:
                        GetEmailInputs(contactChoice);
                        break;
                    default:
                        DisplayContactDetails(contactChoice);
                        break;
                }
            }
        } while (contactChoice.Name != "Go Back");
    }

    public static void DisplayContactDetails(Contact contact)
    {
        Grid grid = new Grid();
        grid.AddColumn();
        grid.AddColumn();

        grid.AddRow("[bold yellow]Name:[/]", contact.Name);
        grid.AddRow("[bold yellow]Group:[/]", contact.Group.ToString());
        grid.AddRow("[bold yellow]Email:[/]", contact.Email);
        grid.AddRow("[bold yellow]Phone:[/]", contact.Phone);

        AnsiConsole.Write(new Panel(grid)
        {
            Header = new PanelHeader("[bold yellow]Contact Details[/]"),
            Border = BoxBorder.Rounded,
            Padding = new Padding(1)
        });

        AnsiConsole.MarkupLine("Press the [yellow]Enter[/] key to continue.");
        Console.ReadLine();
    }

    private async Task GetCreateContactInputs()
    {
        AnsiConsole.MarkupLine($"You've picked Create a Contact.");

        string contactName = AnsiConsole.Prompt(
                                new TextPrompt<string>("What's the contact's name?"));

        string contactGroup = AnsiConsole.Prompt(
                                        new SelectionPrompt<string>()
                                        .Title($"What group does [yellow]{contactName}[/] belong to?")
                                        .AddChoices(new string[] { "Work", "Friends", "Volunteer", "Hobby", "Sport", "Faith" }));

        string contactEmail = AnsiConsole.Prompt(
                                new TextPrompt<string>($"What's [yellow]{contactName}'s[/] email (example@somewhere.com)?")
                                .Validate(e => Validator.ValidateEmail(e) ? ValidationResult.Success() : ValidationResult.Error()));

        string contactPhone = AnsiConsole.Prompt(
                                new TextPrompt<string>($"What's [yellow]{contactName}'s[/] phone number (1234567890)?")
                                .Validate(e => Validator.ValidatePhone(e) ? ValidationResult.Success() : ValidationResult.Error()));

        contactPhone = Formatter.FormatPhoneNumber(contactPhone);

        await _repository.CreateNewContactAsync(
                            new Contact
                            {
                                Name = contactName,
                                Group = ContactGroupExtension.GetGroupFromString(contactGroup),
                                Email = contactEmail,
                                Phone = contactPhone
                            });
    }

    private async Task GetUpdateContactInputs(Contact contact)
    {
        AnsiConsole.MarkupLine($"You've picked Update a Contact.");

        string newName = AnsiConsole.Prompt(
                                new TextPrompt<string>($"Please enter [yellow]{contact.Name}[/]'s new name:"));

        string newGroup = AnsiConsole.Prompt(
                                        new SelectionPrompt<string>()
                                        .Title($"What group does [yellow]{newName}[/] belong to?")
                                        .AddChoices(new string[] { "Work", "Friends", "Volunteer", "Hobby", "Sport", "Faith" }));

        string newEmail = AnsiConsole.Prompt(
                                new TextPrompt<string>($"What's [yellow]{newName}'s[/] email (example@somewhere.com)?")
                                .Validate(e => Validator.ValidateEmail(e) ? ValidationResult.Success() : ValidationResult.Error()));

        string newPhone = AnsiConsole.Prompt(
                                new TextPrompt<string>($"What's [yellow]{newName}'s[/] phone number (1234567890)?")
                                .Validate(e => Validator.ValidatePhone(e) ? ValidationResult.Success() : ValidationResult.Error()));

        newPhone = Formatter.FormatPhoneNumber(newPhone);
        await _repository.UpdateContact(
            new Contact
            {
                Id = contact.Id,
                Name = newName,
                Group = ContactGroupExtension.GetGroupFromString(newGroup),
                Email = newEmail,
                Phone = newPhone
            });
    }

    private async Task DeleteContact(Contact contactChoice)
    {
        await _repository.DeleteContactAsync(contactChoice.Id);
        AnsiConsole.MarkupLine($"You've deleted contact [bold yellow]{contactChoice.Name}[/] with the ID [bold yellow]{contactChoice.Id}[/]. \nPress the [yellow]Enter[/] key to continue.");
        Console.ReadLine();
    }
   
    private void GetEmailInputs(Contact contactChoice)
    {
        string subject = AnsiConsole.Prompt<string>(
                            new TextPrompt<string>("What is the email's subject?")
                            .Validate<string>(message => 
                                message.Length < 900 
                                ? ValidationResult.Success() 
                                : ValidationResult.Error("[maroon]Subject is too long.[/]")));

        string body = AnsiConsole.Prompt<string>(
                            new TextPrompt<string>("What is the email's body (message)?")
                            .Validate(message =>
                                message.Length < 15000
                                ? ValidationResult.Success()
                                : ValidationResult.Error("[maroon]Your message is too long.[/]")));

        AnsiConsole.MarkupLine(_emailService.SendEmail(contactChoice.Email, subject, body));

        AnsiConsole.MarkupLine($"\nPress the [bold yellow]Enter[/] key to continue.");
        Console.ReadLine();
    }
}