using Microsoft.Extensions.DependencyInjection;
using PhoneBook.Data;
using PhoneBook.Services;
using PhoneBook.Views;

class Program
{
    public static async Task Main(string[] args)
    {
        var services = new ServiceCollection();
        services.AddDbContext<PhonebookContext>();
        services.AddScoped<PhonebookRepository>();
        services.AddSingleton<PhonebookView>();
        services.AddTransient<EmailService>(provider => new EmailService(
                                            "smtp.gmail.com",
                                            587,
                                            Environment.GetEnvironmentVariable("GMAIL_USERNAME_TESTING") ?? "",
                                            Environment.GetEnvironmentVariable("GMAIL_PASSWORD_TESTING") ?? ""));

        ServiceProvider serviceProvier = services.BuildServiceProvider();

        PhonebookView phonebookView = serviceProvier.GetRequiredService<PhonebookView>();
        await phonebookView.ShowMainMenu();
    }
}


    
