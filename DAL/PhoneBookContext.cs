using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using PhoneBook.Models;

namespace PhoneBook.DAL
{
    public class PhoneBookContext : DbContext
    {
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
    }
}