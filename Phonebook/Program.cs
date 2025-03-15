using Microsoft.Extensions.DependencyInjection;
using PhoneBook.Data;
using PhoneBook.Views;

class Program
{
    public static async Task Main(string[] args)
    {
        var services = new ServiceCollection();
        services.AddDbContext<PhonebookContext>();
        services.AddScoped<PhonebookRepository>();
        services.AddSingleton<PhonebookView>();

        ServiceProvider serviceProvier = services.BuildServiceProvider();

        PhonebookView phonebookView = serviceProvier.GetRequiredService<PhonebookView>();
        await phonebookView.ShowMainMenu();
    }
}


    
