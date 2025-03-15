using Microsoft.Extensions.DependencyInjection;
using PhoneBook.oksanatk.Data;

class Program
{
    public static void Main(string[] args)
    {
        var services = new ServiceCollection();
        services.AddDbContext<PhonebookContext>();

        var serviceProvier = services.BuildServiceProvider();
    }
}


    
