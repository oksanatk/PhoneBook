using PhoneBook.Data;
using PhoneBook.Models;
using Spectre.Console;

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
            
            AnsiConsole.MarkupLine($"[yellow]You picked {mainMenuChoice}[/]");
            Console.ReadLine();
        } while (mainMenuChoice != "Quit");
    }

    public async Task DisplayAllContacts()
    {
        string contactChoice = "";
        List<Contact> allContacts = await _repository.ReadAllContactsAsync();
        List<string> menuSelection = allContacts.Select(c => c.Name).ToList();
        menuSelection.Add("Go Back");
        do
        {
            contactChoice = AnsiConsole.Prompt(
                                    new SelectionPrompt<string>()
                                    .Title("Choose the contact to view.")
                                    .AddChoices(menuSelection)
                                    );
        } while (contactChoice != "Go Back"); 
    }    
    private async Task CreateContactInputs()
    {
        AnsiConsole.MarkupLine($"You've picked Create a Contact.");
        // any way for me to make a form here? nope, but I can use lots of prompts

        string contactName = AnsiConsole.Prompt(
                                new TextPrompt<string>("What's the contact's name?"));

        ContactGroup contactGroup = AnsiConsole.Prompt(
                                        new TextPrompt<ContactGroup>($"What group does {contactName} belong to?")
                                        //.AddChoices<>);

        
    }
}
