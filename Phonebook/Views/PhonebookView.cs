using PhoneBook.Data;
using PhoneBook.Models;
using Spectre.Console;
using PhoneBook.Utilities;

namespace PhoneBook.Views;

class PhonebookView
{
    private readonly PhonebookRepository _repository;

    public PhonebookView(PhonebookRepository repository)
    {
        _repository = repository;
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
                            "Send SMS",
                            "Quit"})
                            .MoreChoicesText("[grey]Move up and down to view more options[/]"));

            switch (mainMenuChoice)
            {
                case "View Contacts":
                    await DisplayAllContacts();
                    break;
                case "Create Contact":
                    await CreateContactInputs();
                    break;
                case "Edit Contacts":
                    break;
                case "Delete Contact":
                    break;
                case "Send Email":
                    break;
                case "Send SMS":
                    break;
                case "Quit":
                    break;
                default:
                    AnsiConsole.MarkupLine("[maroon]There seems to have been some type of issue. Press Enter to try again.[/]");
                    Console.ReadLine();
                    break;
            }
            Console.WriteLine($"You've picked {mainMenuChoice}. Press the Enter key to continue.");
            Console.ReadLine();
        } while (mainMenuChoice != "Quit");
    }

    public async Task DisplayAllContacts()
    {
        Contact contactChoice;
        List<Contact> allContacts = await _repository.ReadAllContactsAsync();
        allContacts.Add(new Contact { Name = "Go Back" });

        do
        {
            Console.Clear();
            contactChoice = AnsiConsole.Prompt<Contact>(
                                    new SelectionPrompt<Contact>()
                                        .Title($"Choose the contact to view.")
                                        .AddChoices(allContacts)
                                        .UseConverter<Contact>(c => c.Name)
                                        );
            if (contactChoice.Name != "Go Back")
            {
                DisplayContactDetails(contactChoice);
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

    private async Task CreateContactInputs()
    {
        AnsiConsole.MarkupLine($"You've picked Create a Contact.");
        // any way for me to make a form here? nope, but I can use lots of prompts

        string contactName = AnsiConsole.Prompt(
                                new TextPrompt<string>("What's the contact's name?"));

        string contactGroup = AnsiConsole.Prompt(
                                        new SelectionPrompt<string>()
                                        .Title($"What group does [yellow]{contactName}[/] belong to?")
                                        .AddChoices(new string[] {"Work", "Friends","Volunteer", "Hobby", "Sport","Faith"}));

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
                                Group = Contact.GetGroupFromString(contactGroup),
                                Email = contactEmail,
                                Phone = contactPhone
                            });        
        
    }
}
