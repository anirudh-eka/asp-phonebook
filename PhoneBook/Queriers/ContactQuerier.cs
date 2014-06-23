using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PhoneBook.DAL;
using PhoneBook.Models;

namespace PhoneBook.Queriers
{
    public class ContactQuerier
    {
        private PhoneBookContext db;
        public ContactQuerier(PhoneBookContext db)
        {
            this.db = db;
        }
        public IQueryable<Contact> GetContacts()
        {
            return from contact in db.Contacts select contact;
        }

        public IQueryable<Contact> GetContactsFor(int userId)
        {
            IQueryable<Contact> contacts = GetContacts();
            return contacts.Where(c => c.Owner.UserId == userId);
        }

        public Contact GetContactById(int id)
        {
            return db.Contacts.Find(id);
        }
    }
}
