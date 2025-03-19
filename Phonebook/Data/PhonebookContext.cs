using PhoneBook.Models;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

namespace PhoneBook.Data;

class PhonebookContext : DbContext
{
    public DbSet<Contact> Contacts { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(ConfigurationManager.ConnectionStrings["PhonebookDatabase"].ConnectionString);
    }
}


