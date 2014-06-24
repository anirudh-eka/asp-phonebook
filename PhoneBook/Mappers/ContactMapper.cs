using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PhoneBook.Models;
using PhoneBook.ViewModels;

namespace PhoneBook.Mappers
{
    public class ContactMapper : IMapToExisting<Contact, ContactViewModel>
    {
        public void Map(Contact contact, ContactViewModel contactViewModel, UserProfile owner)
        {
            contact.Name = contactViewModel.Name;
            contact.ID = contactViewModel.ID;
            contact.Number = contactViewModel.Number;
            if (owner != null)
            {
                contact.Owner = owner;
            }
        }

        public void Map(Contact contact, ContactViewModel contactViewModel)
        {
            Map(contact, contactViewModel, null);
        }
    }
}