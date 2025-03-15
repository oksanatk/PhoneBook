using Microsoft.EntityFrameworkCore;
using PhoneBook.oksanatk.Models;
using System.Configuration;

namespace PhoneBook.oksanatk.Data;

class PhonebookContext : DbContext
{
    public DbSet<Contact> Contacts { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(ConfigurationManager.ConnectionStrings["PhonebookDatabase"].ConnectionString);
    }
}


